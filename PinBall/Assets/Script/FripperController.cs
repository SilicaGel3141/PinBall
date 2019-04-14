using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

	private HingeJoint myHingeJoint;

	private float flDefaultAngle = 20;
	private float flFlickAngle = -20;

	private bool boPush = false;

	// ウィンドウ解像度の半分を取得
	private float flScreenWidth = Screen.width / 2;

	// Use this for initialization
	void Start () {
		this.myHingeJoint = GetComponent<HingeJoint>();

		SetAngle( this.flDefaultAngle );

	}
	
	// Update is called once per frame
	void Update () {

		//左矢印キーを押した時左フリッパーを動かす
		if(Input.GetKeyDown( KeyCode.LeftArrow ) && tag == "LeftFripperTag") {
			SetAngle( this.flFlickAngle );
		}
		//右矢印キーを押した時右フリッパーを動かす
		if(Input.GetKeyDown( KeyCode.RightArrow ) && tag == "RightFripperTag") {
			SetAngle( this.flFlickAngle );
		}

		//矢印キー離された時フリッパーを元に戻す
		if(Input.GetKeyUp( KeyCode.LeftArrow ) && tag == "LeftFripperTag") {
			SetAngle( this.flDefaultAngle );
		}
		if(Input.GetKeyUp( KeyCode.RightArrow ) && tag == "RightFripperTag") {
			SetAngle( this.flDefaultAngle );
		}

	}
/*
	int TouchJug() {
		//だめだああああああああああああああああああああ
		// Touchクラスの情報を取得(配列)
		Touch[] TouchInfo = Input.touches;

		int i;
		bool fTapRight, fTapLeft;

		fTapRight = false;
		fTapLeft = false;

		for( i = 0; i < Input.touchCount; i++ ) {
			if( TouchInfo[i].position.x > flScreenWidth ) {
				fTapRight = true;
			} else if( TouchInfo[i].position.x >= 0) {
				fTapLeft = true;
			}
		}

		int inInputJug = 0;

		if( fTapLeft ) {
			inInputJug += 0x1;
		}

		if( fTapRight ) {
			inInputJug += 0x2;
		}

		return inInputJug;
	}

	 KeyJug() {


	}
	*/
	public void SetAngle( float angle ) {

		// 現在値をローカルに取得
		JointSpring jointSpr = this.myHingeJoint.spring;

		// 入力するアングルをセット
		jointSpr.targetPosition = angle;

		// セットされた値をモデルに反映
		this.myHingeJoint.spring = jointSpr;

	}
}
