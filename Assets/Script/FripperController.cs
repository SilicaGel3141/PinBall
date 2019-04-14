using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

	private HingeJoint myHingeJoint;

	private float pflDefaultAngle = 20;
	private float pflFlickAngle = -20;

	private bool pboPush = false;

	// Keyの前回値保持
	private bool pboBefLeftArrowPos = false;
	private bool pboBefRightArrowPos = false;

	// ウィンドウ解像度の半分を取得
	private float pflScreenWidth = Screen.width / 2;

	// Use this for initialization
	void Start () {
		this.myHingeJoint = GetComponent<HingeJoint>();

		SetAngle( this.pflDefaultAngle );

	}


	// Update is called once per frame
	void Update () {

		bool boFripMove =false;


		// 呼び出し元のtagによって判定するFripを変更
		if( tag == "LeftFripperTag" ) {
			boFripMove = FlipJug( 1 );

		} else if( tag == "RightFripperTag" ) {
			boFripMove = FlipJug( 2 );
		}

		// 入力に応じてFripに動作を反映
		if( boFripMove ) {
			SetAngle( this.pflFlickAngle );
		} else {
			SetAngle( this.pflDefaultAngle );
		}

	}

	bool FlipJug( int inPosition ) {
		// inPosition
		//  1:LeftFrip
		//  2:RightFrip

		// キーボードの考え方はDownでセットとUpでクリア

		bool boKey, boTouch, boFinalJug;


		boKey = KeyJug( inPosition );
		boTouch = TouchJug( inPosition );

		if( boKey || boTouch ) {
			boFinalJug = true;
		} else {
			boFinalJug = false;
		}

		return boFinalJug;
	}

	bool KeyJug( int inPosition ) {
		// inPosition
		//  1:LeftFrip
		//  2:RightFrip

		// キーボードの考え方はDownでセットとUpでクリア
		bool boKeyDown, boKeyUp, boKeyJug;

		switch( inPosition ) {
			// LeftFrip
			case 1:
				boKeyDown = Input.GetKeyDown( KeyCode.LeftArrow );
				boKeyUp = Input.GetKeyUp( KeyCode.LeftArrow );
				boKeyJug = KeyJugInsp( boKeyDown, boKeyUp, pboBefLeftArrowPos );
				pboBefLeftArrowPos = boKeyJug;
				break;
			// RightFrip
			case 2:
				boKeyDown = Input.GetKeyDown( KeyCode.RightArrow );
				boKeyUp = Input.GetKeyUp( KeyCode.RightArrow );
				boKeyJug = KeyJugInsp( boKeyDown, boKeyUp, pboBefRightArrowPos );
				pboBefRightArrowPos = boKeyJug;
				break;
			// Else
			default:
				boKeyJug = false;
				break;
		}

		return boKeyJug;
	}

	bool KeyJugInsp( bool boDown, bool boUp, bool boOld ) {

		bool boKeyFrip;

		if( boDown ) {
			// Downの入力でFripを上げる判定
			boKeyFrip = true;
		} else if( boUp ) {
			// Upの入力でFripを下げる判定
			boKeyFrip = false;
		} else {
			boKeyFrip = boOld;
		}
		return boKeyFrip;
	}

	bool TouchJug( int inPosition ) {
		// Touchクラスの情報を取得(配列)
		Touch[] touchInfo = Input.touches;

		int i;
		bool boTapRight, boTapLeft, boTapJug;

		boTapRight = false;
		boTapLeft = false;

		//タップ情報からx軸に応じてタップがあるかを判定
		for( i = 0; i < Input.touchCount; i++ ) {
			if( touchInfo[i].position.x > pflScreenWidth ) {
				boTapRight = true;
			} else if( touchInfo[i].position.x >= 0) {
				boTapLeft = true;
			}
		}

		switch( inPosition ) {
			case 1:
				boTapJug = boTapLeft;
				break;
			case 2:
				boTapJug = boTapRight;
				break;
			default:
				boTapJug = false;
				break;

		}

		return boTapJug;
	}

	public void SetAngle( float angle ) {

		// 現在値をローカルに取得
		JointSpring jointSpr = this.myHingeJoint.spring;

		// 入力するアングルをセット
		jointSpr.targetPosition = angle;

		// セットされた値をモデルに反映
		this.myHingeJoint.spring = jointSpr;

	}
}
