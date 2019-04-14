using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallController : MonoBehaviour {

	private float visiblePosZ = -6.5f;

	private GameObject gameoverText;
	private GameObject scoreText;

	private int score_smlstr = 10;					/* 小さい星 */
	private int score_lgestr = 20;					/* 大きい星 */
	private int score_smlcld = 5;					/* 小さい雲 */
	private int score_lgecld = 15;					/* 大きい雲 */
	private int totalscore = 0;						/* 合計スコア */

	void Start () {
		this.gameoverText = GameObject.Find( "GameOverText" );
		this.scoreText = GameObject.Find( "ScoreText" );
	}
	
	void Update () {
		if( this.transform.position.z < this.visiblePosZ ) {
			this.gameoverText.GetComponent<Text>().text = "Game Over";
		}
	}

	void OnCollisionEnter(Collision collision) {
		bool f_disp_ref;							/* テキスト更新有FLG */
		string dtc_tag = collision.collider.tag;	/* 衝突対象のTagを取得 */
		string disp_score = "Score：";				/* 画面表示文字列 */

		f_disp_ref = true;							/* デフォルトはテキスト更新有に設定 */
		switch( dtc_tag ) {							/* 衝突対象に応じてスコアを加算 */
			case "SmallStarTag":
				totalscore += score_smlstr;
				break;
			case "LargeStarTag":
				totalscore += score_lgestr;
				break;
			case "SmallCloudTag":
				totalscore += score_smlcld;
				break;
			case "LargeCloudTag":
				totalscore += score_lgecld;
				break;
			default:								/* 壁・フリッパの時はdefault */
				f_disp_ref = false;					/* テキスト更新不要 */
				break;
		}
		if( f_disp_ref ) {							/* テキスト更新が必要？ */
			if( totalscore > 1000 ) {				/* 仕様なしだがOVF防止 */
				totalscore = 1001;
				disp_score += "1000over";
			} else {
				disp_score += totalscore;
			}

			this.scoreText.GetComponent<Text>().text = disp_score;
													/* 画面に表示 */
		}
	}
}
