using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

	private HingeJoint myHingeJoint;

	private float defaultAngle = 20;
	private float flickAngle = -20;

	// Use this for initialization
	void Start () {
		this.myHingeJoint = GetComponent<HingeJoint>();

		SetAngle( this.defaultAngle );
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripperTag" ) {
			SetAngle( this.flickAngle );
		}
		if (Input.GetKeyDown( KeyCode.RightArrow ) && tag == "RightFripperTag") {
			SetAngle( this.flickAngle );
		}

		if (Input.GetKeyUp( KeyCode.LeftArrow ) && tag == "LeftFripperTag") {
			SetAngle( this.defaultAngle );
		}
		if (Input.GetKeyUp( KeyCode.RightArrow ) && tag == "RightFripperTag") {
			SetAngle( this.defaultAngle );
		}

	}

	public void SetAngle( float angle ) {
		JointSpring jointSpr = this.myHingeJoint.spring;	/* 現在値をローカルに取得 */
		jointSpr.targetPosition = angle;					/* 入力するアングルをセット */
		this.myHingeJoint.spring = jointSpr;				/* セットされた値をモデルに反映 */

	}
}
