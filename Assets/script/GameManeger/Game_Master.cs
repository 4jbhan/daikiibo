﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Master : MonoBehaviour
{
    //使用するスクリプトのインスタンス
    Game_Data g_data;
    User_Interface u_i;
    Object_Mover o_mov;

    //ハイライト用エフェクト
    public GameObject Prefab_Effect;
    public GameObject Damage_Effect;


    //マーカーに使用するオブジェクト
    public GameObject marker;

    //tmpobj = 移動させた駒の保持用　obj = 現在のクリック対象
    GameObject tmpobj = null, obj = null;

    //各クリスタルのステータス
    public static Crystal_Status[] crystat = new Crystal_Status[34];
    
    //盤面の状態
    public static int[,] CryPosition = new int[8, 8];

    //攻撃対象保持リスト
    public List<int> targets = new List<int>();


    //残存青駒
    public List<int> ours = new List<int>();

    //COM用
    //残存敵駒
    public List<int> enemys = new List<int>();
    //移動可能な座標
    public List<int> movablex = new List<int>();
    public List<int> movabley = new List<int>();
    //乱数
    System.Random rand = new System.Random();

    //各フラグ
    //marked マーカー展開中→true
    //moved 移動済み→true
    //eturn 赤のターン→true
    //pvp 対人→true
    //ongame ゲーム中→true
    public bool marked = false, moved = false, eturn = false, pvp, ongame = true;
   public static bool first = true;
    bool waiter = false;

    //選択中のオブジェクトのidを取得
    int id;

    //textmesh
    //Use these for Hp,atk text
    public TextMesh Text_HpAtk1;
    public TextMesh Text_HpAtk2;
    public TextMesh Text_HpAtk3;
    public TextMesh Text_HpAtk4;
    public TextMesh Text_HpAtk5;
    public TextMesh Text_HpAtk6;
    public TextMesh Text_HpAtk7;
    public TextMesh Text_HpAtk8;
    public TextMesh Text_HpAtk9;
    public TextMesh Text_HpAtk10;
    public TextMesh Text_HpAtk11;
    public TextMesh Text_HpAtk12;
    public TextMesh Text_HpAtk13;
    public TextMesh Text_HpAtk14;
    public TextMesh Text_HpAtk15;
    public TextMesh Text_HpAtk16;
    public TextMesh Text_HpAtk17;
    public TextMesh Text_HpAtk18;
    public TextMesh Text_HpAtk19;
    public TextMesh Text_HpAtk20;
    public TextMesh Text_HpAtk21;
    public TextMesh Text_HpAtk22;
    public TextMesh Text_HpAtk23;
    public TextMesh Text_HpAtk24;
    public TextMesh Text_HpAtk25;
    public TextMesh Text_HpAtk26;
    public TextMesh Text_HpAtk27;
    public TextMesh Text_HpAtk28;
    public TextMesh Text_HpAtk29;
    public TextMesh Text_HpAtk30;
    public TextMesh Text_HpAtk31;
    public TextMesh Text_HpAtk32;



    TextMesh[] inscene = new TextMesh[33];


    public Text turnmanage;

    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        //他スクリプトのインスタンスを取得
        g_data = GetComponent<Game_Data>();
        u_i = GetComponent<User_Interface>();
        o_mov = GetComponent<Object_Mover>();


        // Debug.Log(first);
        //各種データ初期化

        g_data.RawPossetter();
        g_data.DicSetter();

        if (first)
        {
            first = false;
            g_data.StatSetter(crystat);
            g_data.CryPosSetter(CryPosition);

            Text_HpAtk1.text = "King\nH" + crystat[1].hp + ":A" + crystat[1].atk;
            Text_HpAtk2.text = "Queen\nH" + crystat[2].hp + ":A" + crystat[2].atk;
            Text_HpAtk3.text = "Bishop\nH" + crystat[3].hp + ":A" + crystat[3].atk;
            Text_HpAtk4.text = "Bishop\nH" + crystat[4].hp + ":A" + crystat[4].atk;
            Text_HpAtk5.text = "knight\nH" + crystat[5].hp + ":A" + crystat[5].atk;
            Text_HpAtk6.text = "knight\nH" + crystat[6].hp + ":A" + crystat[6].atk;
            Text_HpAtk7.text = "Rook\nH" + crystat[7].hp + ":A" + crystat[7].atk;
            Text_HpAtk8.text = "Rook\nH" + crystat[8].hp + ":A" + crystat[8].atk;
            Text_HpAtk9.text = "Pawn\nH" + crystat[9].hp + ":A" + crystat[9].atk;
            Text_HpAtk10.text = "Pawn\nH" + crystat[10].hp + ":A" + crystat[10].atk;
            Text_HpAtk11.text = "Pawn\nH" + crystat[11].hp + ":A" + crystat[11].atk;
            Text_HpAtk12.text = "Pawn\nH" + crystat[12].hp + ":A" + crystat[12].atk;
            Text_HpAtk13.text = "Pawn\nH" + crystat[13].hp + ":A" + crystat[13].atk;
            Text_HpAtk14.text = "Pawn\nH" + crystat[14].hp + ":A" + crystat[14].atk;
            Text_HpAtk15.text = "Pawn\nH" + crystat[15].hp + ":A" + crystat[15].atk;
            Text_HpAtk16.text = "Pawn\nH" + crystat[16].hp + ":A" + crystat[16].atk;
            Text_HpAtk17.text = "king\nH" + crystat[17].hp + ":A" + crystat[17].atk;
            Text_HpAtk18.text = "Queen\nH" + crystat[18].hp + ":A" + crystat[18].atk;
            Text_HpAtk19.text = "Bishop\nH" + crystat[19].hp + ":A" + crystat[19].atk;
            Text_HpAtk20.text = "Bishop\nH" + crystat[20].hp + ":A" + crystat[20].atk;
            Text_HpAtk21.text = "knight\nH" + crystat[21].hp + ":A" + crystat[21].atk;
            Text_HpAtk22.text = "knight\nH" + crystat[22].hp + ":A" + crystat[22].atk;
            Text_HpAtk23.text = "Rook\nH" + crystat[23].hp + ":A" + crystat[23].atk;
            Text_HpAtk24.text = "Rook\nH" + crystat[24].hp + ":A" + crystat[24].atk;
            Text_HpAtk25.text = "Pawn\nH" + crystat[25].hp + ":A" + crystat[25].atk;
            Text_HpAtk26.text = "Pawn\nH" + crystat[26].hp + ":A" + crystat[26].atk;
            Text_HpAtk27.text = "Pawn\nH" + crystat[27].hp + ":A" + crystat[27].atk;
            Text_HpAtk28.text = "Pawn\nH" + crystat[28].hp + ":A" + crystat[28].atk;
            Text_HpAtk29.text = "Pawn\nH" + crystat[29].hp + ":A" + crystat[29].atk;
            Text_HpAtk30.text = "Pawn\nH" + crystat[30].hp + ":A" + crystat[30].atk;
            Text_HpAtk31.text = "Pawn\nH" + crystat[31].hp + ":A" + crystat[31].atk;
            Text_HpAtk32.text = "Pawn\nH" + crystat[32].hp + ":A" + crystat[32].atk;

        }


        pvp = StartMenu.RetPvp();

        //駒の初期配置
        o_mov.ArraytoPos(CryPosition);


        var hand = GameObject.Find("Text_HpAtk1");
        inscene[1] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk2");
        inscene[2] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk3");
        inscene[3] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk4");
        inscene[4] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk5");
        inscene[5] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk6");
        inscene[6] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk7");
        inscene[7] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk8");
        inscene[8] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk9");
        inscene[9] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk10");
        inscene[10] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk11");
        inscene[11] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk12");
        inscene[12] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk13");
        inscene[13] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk14");
        inscene[14] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk15");
        inscene[15] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk16");
        inscene[16] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk17");
        inscene[17] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk18");
        inscene[18] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk19");
        inscene[19] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk20");
        inscene[20] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk21");
        inscene[21] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk22");
        inscene[22] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk23");
        inscene[23] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk24");
        inscene[24] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk25");
        inscene[25] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk26");
        inscene[26] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk27");
        inscene[27] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk28");
        inscene[28] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk29");
        inscene[29] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk30");
        inscene[30] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk31");
        inscene[31] = hand.GetComponent<TextMesh>();
        hand = GameObject.Find("Text_HpAtk32");
        inscene[32] = hand.GetComponent<TextMesh>();

        foreach (var cry in crystat)
        {
            if (cry.hp == 0)
            {
                var handl = GameObject.Find(cry.name);
                Destroy(handl);
            }
        }

        /*  if (!pvp)//残存敵駒の設定
          {*/

        ours.Add(1);
        ours.Add(2);
        ours.Add(3);
        ours.Add(4);
        ours.Add(5);
        ours.Add(6);
        ours.Add(7);
        ours.Add(8);
        ours.Add(9);
        ours.Add(10);
        ours.Add(11);
        ours.Add(12);
        ours.Add(13);
        ours.Add(14);
        ours.Add(15);
        ours.Add(16);

        enemys.Add(17);
        enemys.Add(18);
        enemys.Add(19);
        enemys.Add(20);
        enemys.Add(21);
        enemys.Add(22);
        enemys.Add(23);
        enemys.Add(24);
        enemys.Add(25);
        enemys.Add(26);
        enemys.Add(27);
        enemys.Add(28);
        enemys.Add(29);
        enemys.Add(30);
        enemys.Add(31);
        enemys.Add(32);
        //        }

        turnmanage.text = "Blue Crystal Turn";
        turnmanage.color = Color.blue;
    }

    void Update()
    {
        if (ongame)
        {

            if (!eturn || (pvp && eturn))//COM戦かつ青ターンまたは対人戦ならクリック対応の動作
            {
                

                obj = u_i.getClickObject();//クリックしたオブジェクトの取得
                if (obj != null)//取得成功したら
                {
                    if (!moved && !marked && obj != null && obj.name.IndexOf("Marker") < 0 && obj.name != "planet" && obj.name.IndexOf("object") < 0)
                    {//マーカー展開
                        //対象のid取得
                        id = g_data.NametoId[obj.name];

                        if (!eturn && crystat[id].iff)//青ターン時はクリック対象が青駒である場合のみ許可
                        {
                            MrkGenerator(crystat[id], CryPosition, pvp, eturn);
                            mListResetter();//COM用に設定されてしまったリストをリセット
                            tmpobj = obj;//クリック中オブジェクトを保持(実際に移動するときに必要)
                            marked = true;
                        }
                        else if (eturn && !crystat[id].iff)//赤ターン時はクリック対象が赤ゴマのみ許可
                        {
                            MrkGenerator(crystat[id], CryPosition, pvp, eturn);
                            mListResetter();
                            tmpobj = obj;
                            marked = true;
                        }
                    }
                    else if (!moved && marked && obj.name.IndexOf("Marker") >= 0)
                    {//マーカークリック　移動
                        MrkRemover();//マーカー排除
                        marked = false;

                        id = g_data.NametoId[tmpobj.name];

                        //移動処理

                        var tox = int.Parse(obj.name.Substring(0, 1));//クリックしたマーカーのx座標
                        var toy = int.Parse(obj.name.Substring(1, 1));//クリックしたマーカーのy座標

                        //実際にオブジェクトの座標を変更
                        var rigid = tmpobj.GetComponent<Rigidbody>();
                        rigid.GetComponent<Rigidbody>().MovePosition(g_data.rawPosition[tox, toy]);


                        //各種パラメータの更新
                        CryPosition[crystat[id].y, crystat[id].x] = 0;
                        crystat[id].y = tox;
                        crystat[id].x = toy;
                        CryPosition[crystat[id].y, crystat[id].x] = id;

                        moved = true;

                        if (!canAtkCheck(crystat[id], CryPosition))//移動先の位置から攻撃動作が可能か判定,falseなら不可能
                        {
                            moved = false;
                            if (!eturn)
                            {//青ターンなら赤ターンへ遷移
                                eturn = true;

                                turnmanage.text = "Red Crystal Turn";
                                turnmanage.color = Color.red;
                            }
                            else//逆
                            {
                                eturn = false;

                                turnmanage.text = "Blue Crystal Turn";
                                turnmanage.color = Color.blue;
                            }
                        }
                    }
                    else if (!moved && marked && obj.name.IndexOf("Marker") < 0 && tmpobj.name != obj.name)
                    {//マーカーキャンセル
                        MrkRemover();
                        marked = false;
                    }
                    else if (!moved && marked && tmpobj.name == obj.name)
                    {//移動放棄
                        MrkRemover();
                        marked = false;

                        moved = true;

                        //その位置から攻撃動作が可能か判定,falseなら不可能
                        if (!canAtkCheck(crystat[id], CryPosition))
                        {
                            moved = false;
                            if (!eturn)
                            {//青ターンなら赤ターンへ遷移
                                eturn = true;

                                turnmanage.text = "Red Crystal Turn";
                                turnmanage.color = Color.red;
                            }
                            else//逆
                            {
                                eturn = false;

                                turnmanage.text = "Blue Crystal Turn";
                                turnmanage.color = Color.blue;
                            }
                        }
                    }
                    else if (moved && obj.name.IndexOf("Marker") < 0 && obj.name != "planet" && obj.name.IndexOf("object") < 0)
                    {//攻撃動作
                        //id = 攻撃側オブジェクト
                        //did = 防御側オブジェクト
                        id = g_data.NametoId[tmpobj.name];
                        int did = g_data.NametoId[obj.name];

                        if ((eturn && crystat[did].iff || !eturn && !crystat[did].iff) && crystat[did].targeted)
                        {//ターンと攻撃駒に応じた適切な攻撃対象か判定
                            //パラメータリセット
                            EffectRemover();
                            tListResetter();

                            //hpの変動
                            crystat[did].hp -= crystat[id].atk;
                            var eff = Instantiate(Damage_Effect, g_data.rawPosition[crystat[did].y, crystat[did].x], Quaternion.identity);
                            DestroyObject(eff, 2f);

                            if (crystat[did].hp <= 0)//防御側のHPが0を下回るなら
                            {
                                //盤面の状態から破壊された駒を取り除く
                                CryPosition[crystat[did].y, crystat[did].x] = 0;

                                //オブジェクトを消去
                                GameObject hand = GameObject.Find(crystat[did].name);
                                Destroy(hand);

                                if (crystat[did].tag == 0)//破壊された駒がキングならゲーム終了
                                {
                                    ongame = false;
                                }
                                if (!crystat[did].iff)//com戦なら破壊した駒を残存駒から除く
                                {
                                    enemys.Remove(did);
                                }
                                else if (crystat[did].iff)//破壊した駒を残存駒から除く
                                {
                                    ours.Remove(did);
                                }
                            }

                            moved = false;

                            if (!eturn)
                            {//青ターンなら赤ターンへ遷移
                                eturn = true;

                                turnmanage.text = "Red Crystal Turn";
                                turnmanage.color = Color.red;
                            }
                            else//逆
                            {
                                eturn = false;

                                turnmanage.text = "Blue Crystal Turn";
                                turnmanage.color = Color.blue;
                            }
                        }
                    }
                }
               
            }
            else if (!pvp && eturn)//COMターン
            {
                int aid = -1, did;

              /*  StartCoroutine(Sample());
                while (!waiter)
                { }
                waiter = true;
                */
                foreach (var value in enemys)
                {
                    if (canAtkCheck(crystat[value], CryPosition))
                    {
                        aid = value;
                        break;
                    }
                }

                if (aid != -1)
                {
                    //tag基準で昇順ソート
                    //その先頭 = 最優先目標 
                    targets.Sort(TagCompare);
                    did = targets[0];

                    crystat[did].hp -= crystat[aid].atk;
                    var eff = Instantiate(Damage_Effect, g_data.rawPosition[crystat[did].y, crystat[did].x], Quaternion.identity);
                    DestroyObject(eff, 2f);



                    if (crystat[did].hp <= 0)//破壊処理
                    {
                        CryPosition[crystat[did].y, crystat[did].x] = 0;
                        GameObject handler = GameObject.Find(crystat[did].name);
                        Destroy(handler);
                        if (crystat[did].tag == 0)//破壊された駒がキングならゲーム終了
                        {
                            ongame = false;
                        }
                        ours.Remove(did);
                    }

                }
                else
                {
                    int c = 0;
                    do
                    {
                        mListResetter();
                        c++;
                        aid = enemys[(int)rand.Next(enemys.Count - 1)];
                        MrkGenerator(crystat[aid], CryPosition, pvp, eturn);
                    } while (movablex.Count <= 0 || (c <= 2 && crystat[aid].tag != 5));//移動可能な駒をランダムで選択

                    //移動座標をランダムで選択
                    int p = rand.Next(movablex.Count - 1);
                    int ax = movablex[p];
                    int ay = movabley[p];

                    //移動可能マスのリセット
                    mListResetter();


                    //実際の移動処理
                    GameObject hand = GameObject.Find(crystat[aid].name);
                    var rigid = hand.GetComponent<Rigidbody>();
                    rigid.GetComponent<Rigidbody>().MovePosition(g_data.rawPosition[ay, ax]);


                    CryPosition[crystat[aid].y, crystat[aid].x] = 0;
                    crystat[aid].y = ay;
                    crystat[aid].x = ax;
                    CryPosition[crystat[aid].y, crystat[aid].x] = aid;

                    if (canAtkCheck(crystat[aid], CryPosition))
                    {//移動先から攻撃可能か判定


                        targets.Sort(TagCompare);
                        did = targets[0];

                        crystat[did].hp -= crystat[aid].atk;
                        var eff = Instantiate(Damage_Effect, g_data.rawPosition[crystat[did].y, crystat[did].x], Quaternion.identity);
                        DestroyObject(eff, 2f);

                        if (crystat[did].hp <= 0)//破壊処理
                        {
                            CryPosition[crystat[did].y, crystat[did].x] = 0;
                            GameObject handler = GameObject.Find(crystat[did].name);
                            Destroy(handler);
                            if (crystat[did].tag == 0)//破壊された駒がキングならゲーム終了
                            {
                                ongame = false;
                            }
                            ours.Remove(did);
                        }
                    }
                }
                //エフェクト排除
                EffectRemover();
                //対象リストリセット
                tListResetter();
                eturn = false;
                

                turnmanage.text = "Blue Crystal Turn";
                turnmanage.color = Color.blue;


                //textmesh更新
                //Display Hp,Atk text

                if (ours.IndexOf(1) >= 0)
                {
                    Text_HpAtk1.text = "King\nH" + crystat[1].hp + ":A" + crystat[1].atk;
                }

                if (ours.IndexOf(2) >= 0)
                {
                    Text_HpAtk2.text = "Queen\nH" + crystat[2].hp + ":A" + crystat[2].atk;
                }
                if (ours.IndexOf(3) >= 0)
                {
                    Text_HpAtk3.text = "Bishop\nH" + crystat[3].hp + ":A" + crystat[3].atk;
                }
                if (ours.IndexOf(4) >= 0)
                {
                    Text_HpAtk4.text = "Bishop\nH" + crystat[4].hp + ":A" + crystat[4].atk;
                }
                if (ours.IndexOf(5) >= 0)
                {
                    Text_HpAtk5.text = "knight\nH" + crystat[5].hp + ":A" + crystat[5].atk;
                }
                if (ours.IndexOf(6) >= 0)
                {
                    Text_HpAtk6.text = "knight\nH" + crystat[6].hp + ":A" + crystat[6].atk;
                }
                if (ours.IndexOf(7) >= 0)
                {
                    Text_HpAtk7.text = "Rook\nH" + crystat[7].hp + ":A" + crystat[7].atk;
                }
                if (ours.IndexOf(8) >= 0)
                {
                    Text_HpAtk8.text = "Rook\nH" + crystat[8].hp + ":A" + crystat[8].atk;
                }
                if (ours.IndexOf(9) >= 0)
                {
                    Text_HpAtk9.text = "Pawn\nH" + crystat[9].hp + ":A" + crystat[9].atk;
                }
                if (ours.IndexOf(10) >= 0)
                {
                    Text_HpAtk10.text = "Pawn\nH" + crystat[10].hp + ":A" + crystat[10].atk;
                }
                if (ours.IndexOf(11) >= 0)
                {
                    Text_HpAtk11.text = "Pawn\nH" + crystat[11].hp + ":A" + crystat[11].atk;

                }
                if (ours.IndexOf(12) >= 0)
                {
                    Text_HpAtk12.text = "Pawn\nH" + crystat[12].hp + ":A" + crystat[12].atk;
                }
                if (ours.IndexOf(13) >= 0)
                {
                    Text_HpAtk13.text = "Pawn\nH" + crystat[13].hp + ":A" + crystat[13].atk;
                }
                if (ours.IndexOf(14) >= 0)
                {
                    Text_HpAtk14.text = "Pawn\nH" + crystat[14].hp + ":A" + crystat[14].atk;
                }
                if (ours.IndexOf(15) >= 0)
                {
                    Text_HpAtk15.text = "Pawn\nH" + crystat[15].hp + ":A" + crystat[15].atk;
                }
                if (ours.IndexOf(16) >= 0)
                {
                    Text_HpAtk16.text = "Pawn\nH" + crystat[16].hp + ":A" + crystat[16].atk;
                }
                if (enemys.IndexOf(17) >= 0)
                {
                    Text_HpAtk17.text = "king\nH" + crystat[17].hp + ":A" + crystat[17].atk;
                }
                if (enemys.IndexOf(18) >= 0)
                {

                    Text_HpAtk18.text = "Queen\nH" + crystat[18].hp + ":A" + crystat[18].atk;
                }
                if (enemys.IndexOf(19) >= 0)
                {
                    Text_HpAtk19.text = "Bishop\nH" + crystat[19].hp + ":A" + crystat[19].atk;
                }
                if (enemys.IndexOf(20) >= 0)
                {
                    Text_HpAtk20.text = "bishop\nH" + crystat[20].hp + ":A" + crystat[20].atk;
                }
                if (enemys.IndexOf(21) >= 0)
                {
                    Text_HpAtk21.text = "knight\nH" + crystat[21].hp + ":A" + crystat[21].atk;
                }
                if (enemys.IndexOf(22) >= 0)
                {
                    Text_HpAtk22.text = "knight\nH" + crystat[22].hp + ":A" + crystat[22].atk;
                }
                if (enemys.IndexOf(23) >= 0)
                {
                    Text_HpAtk23.text = "Rook\nH" + crystat[23].hp + ":A" + crystat[23].atk;
                }
                if (enemys.IndexOf(24) >= 0)
                {
                    Text_HpAtk24.text = "Rook\nH" + crystat[24].hp + ":A" + crystat[24].atk;
                }
                if (enemys.IndexOf(25) >= 0)
                {
                    Text_HpAtk25.text = "Pawn\nH" + crystat[25].hp + ":A" + crystat[25].atk;
                }
                if (enemys.IndexOf(26) >= 0)
                {
                    Text_HpAtk26.text = "Pawn\nH" + crystat[26].hp + ":A" + crystat[26].atk;
                }
                if (enemys.IndexOf(27) >= 0)
                {
                    Text_HpAtk27.text = "Pawn\nH" + crystat[27].hp + ":A" + crystat[27].atk;
                }
                if (enemys.IndexOf(28) >= 0)
                {
                    Text_HpAtk28.text = "Pawn\nH" + crystat[28].hp + ":A" + crystat[28].atk;
                }
                if (enemys.IndexOf(29) >= 0)
                {
                    Text_HpAtk29.text = "Pawn\nH" + crystat[29].hp + ":A" + crystat[29].atk;
                }
                if (enemys.IndexOf(30) >= 0)
                {
                    Text_HpAtk30.text = "Pawn\nH" + crystat[30].hp + ":A" + crystat[30].atk;
                }
                if (enemys.IndexOf(31) >= 0)
                {

                    Text_HpAtk31.text = "Pawn\nH" + crystat[31].hp + ":A" + crystat[31].atk;
                }
                if (enemys.IndexOf(32) >= 0)
                {

                    Text_HpAtk32.text = "Pawn\nH" + crystat[32].hp + ":A" + crystat[32].atk;
                }


                //ゲーム画面内のtextmesh更新
                if (ours.IndexOf(1) >= 0)
                {
                    inscene[1].text = "King\nH" + crystat[1].hp + ":A" + crystat[1].atk;
                }
                if (ours.IndexOf(2) >= 0)
                {
                    inscene[2].text = "Queen\nH" + crystat[2].hp + ":A" + crystat[2].atk;
                }
                if (ours.IndexOf(3) >= 0)
                {
                    inscene[3].text = "Bishop\nH" + crystat[3].hp + ":A" + crystat[3].atk;
                }
                if (ours.IndexOf(4) >= 0)
                {
                    inscene[4].text = "Bishop\nH" + crystat[4].hp + ":A" + crystat[4].atk;
                }
                if (ours.IndexOf(5) >= 0)
                {
                    inscene[5].text = "knight\nH" + crystat[5].hp + ":A" + crystat[5].atk;
                }
                if (ours.IndexOf(6) >= 0)
                {
                    inscene[6].text = "knight\nH" + crystat[6].hp + ":A" + crystat[6].atk;
                }
                if (ours.IndexOf(7) >= 0)
                {
                    inscene[7].text = "Rook\nH" + crystat[7].hp + ":A" + crystat[7].atk;
                }
                if (ours.IndexOf(8) >= 0)
                {
                    inscene[8].text = "Rook\nH" + crystat[8].hp + ":A" + crystat[8].atk;
                }
                if (ours.IndexOf(9) >= 0)
                {
                    inscene[9].text = "Pawn\nH" + crystat[9].hp + ":A" + crystat[9].atk;
                }
                if (ours.IndexOf(10) >= 0)
                {
                    inscene[10].text = "Pawn\nH" + crystat[10].hp + ":A" + crystat[10].atk;
                }
                if (ours.IndexOf(11) >= 0)
                {
                    inscene[11].text = "Pawn\nH" + crystat[11].hp + ":A" + crystat[11].atk;
                }
                if (ours.IndexOf(12) >= 0)
                {
                    inscene[12].text = "Pawn\nH" + crystat[12].hp + ":A" + crystat[12].atk;
                }
                if (ours.IndexOf(13) >= 0)
                {
                    inscene[13].text = "Pawn\nH" + crystat[13].hp + ":A" + crystat[13].atk;
                }
                if (ours.IndexOf(14) >= 0)
                {
                    inscene[14].text = "Pawn\nH" + crystat[14].hp + ":A" + crystat[14].atk;
                }
                if (ours.IndexOf(15) >= 0)
                {
                    inscene[15].text = "Pawn\nH" + crystat[15].hp + ":A" + crystat[15].atk;
                }
                if (ours.IndexOf(16) >= 0)
                {
                    inscene[16].text = "Pawn\nH" + crystat[16].hp + ":A" + crystat[16].atk;
                }
                if (enemys.IndexOf(17) >= 0)
                {
                    inscene[17].text = "king\nH" + crystat[17].hp + ":A" + crystat[17].atk;
                }
                if (enemys.IndexOf(18) >= 0)
                {
                    inscene[18].text = "Queen\nH" + crystat[18].hp + ":A" + crystat[18].atk;
                }
                if (enemys.IndexOf(19) >= 0)
                {
                    inscene[19].text = "Bishop\nH" + crystat[19].hp + ":A" + crystat[19].atk;
                }
                if (enemys.IndexOf(20) >= 0)
                {
                    inscene[20].text = "bishop\nH" + crystat[20].hp + ":A" + crystat[20].atk;
                }
                if (enemys.IndexOf(21) >= 0)
                {
                    inscene[21].text = "knight\nH" + crystat[21].hp + ":A" + crystat[21].atk;
                }
                if (enemys.IndexOf(22) >= 0)
                {
                    inscene[22].text = "knight\nH" + crystat[22].hp + ":A" + crystat[22].atk;
                }
                if (enemys.IndexOf(23) >= 0)
                {
                    inscene[23].text = "Rook\nH" + crystat[23].hp + ":A" + crystat[23].atk;
                }
                if (enemys.IndexOf(24) >= 0)
                {
                    inscene[24].text = "Rook\nH" + crystat[24].hp + ":A" + crystat[24].atk;
                }
                if (enemys.IndexOf(25) >= 0)
                {
                    inscene[25].text = "Pawn\nH" + crystat[25].hp + ":A" + crystat[25].atk;
                }
                if (enemys.IndexOf(26) >= 0)
                {
                    inscene[26].text = "Pawn\nH" + crystat[26].hp + ":A" + crystat[26].atk;
                }
                if (enemys.IndexOf(27) >= 0)
                {
                    inscene[27].text = "Pawn\nH" + crystat[27].hp + ":A" + crystat[27].atk;
                }
                if (enemys.IndexOf(28) >= 0)
                {
                    inscene[28].text = "Pawn\nH" + crystat[28].hp + ":A" + crystat[28].atk;
                }
                if (enemys.IndexOf(29) >= 0)
                {
                    inscene[29].text = "Pawn\nH" + crystat[29].hp + ":A" + crystat[29].atk;
                }
                if (enemys.IndexOf(30) >= 0)
                {
                    inscene[30].text = "Pawn\nH" + crystat[30].hp + ":A" + crystat[30].atk;
                }
                if (enemys.IndexOf(31) >= 0)
                {
                    inscene[31].text = "Pawn\nH" + crystat[31].hp + ":A" + crystat[31].atk;
                }
                if (enemys.IndexOf(32) >= 0)
                {
                    inscene[32].text = "Pawn\nH" + crystat[32].hp + ":A" + crystat[32].atk;

                }
            }

        }



    }


    void OnGUI()
    {
        if (!ongame)
        {
            float sw = Screen.width;
            float sh = Screen.height;

            if (!eturn)
            {
                if (GUI.Button(new Rect(sw * 2 / 5, sh * 2 / 5, sw / 5, sh / 10), "Red Win"))
                {
                    SceneManager.LoadScene("Title");
                }
            }
            else
            {
                if (GUI.Button(new Rect(sw * 2 / 5, sh * 2 / 5, sw / 5, sh / 10), "Blue Win"))
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }
    }




    //AI用待機時間設定

    private IEnumerator Sample()
    {
        yield return new WaitForSeconds(1.0f);
        waiter = true;
    }


    //ソート用比較関数
    public int TagCompare(int a, int b){
        return crystat[a].tag.CompareTo(crystat[b].tag);
    }

    //別シーンでの盤面の取得
    public static int[,] getField()
    {
        return CryPosition;
    }


    //攻撃可能かの判定
    //設定された攻撃可能マスを参照し敵の駒があればtrueを返し,一つもなければfalse
    //同時にエフェクトの展開も行う
    //攻撃対象となりうる駒はリストに保持、.targetedをtrueへ変更
    //以下can○○Checkは同様の動作
    //同じ攻撃範囲の駒はスクリプトを転用してある(一応ものじたいはコメント内にあるので独自に設定したければコメントはずせばおｋ)
    public bool canKingCheck(Crystal_Status attcker, int[,] CryPosition)
    {
        bool flag = false;
        if (attcker.iff)//攻撃側が青
        {
            //右マス (y ,(x + 1)%8)  
            //左マス (y ,(x + 7)%8)　
            //右上マス (y + 1 ,(x + 1)%8)
            //左上マス (y + 1 ,(x + 7)%8)
            //上マス (y + 1 ,x)
            //右下マス (y - 1 ,(x + 1)%8)
            //左下マス (y - 1 ,(x + 7)%8)
            //下マス (y - 1 ,x)

            //id > 16　= 赤駒
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, attcker.x] > 16)
            {
                //攻撃対象となりうる駒をリストに保持
                targets.Add(CryPosition[attcker.y + 1, attcker.x]);
                //.targetedをtrueへ変更 
                //.targetedを参照することでその駒を攻撃していいか判定可能
                crystat[CryPosition[attcker.y + 1, attcker.x]].targeted = true;

                //エフェクト展開(排除は別スクリプトで)
                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, attcker.x], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, attcker.x] > 16)
            {
                targets.Add(CryPosition[attcker.y - 1, attcker.x]);
                crystat[CryPosition[attcker.y - 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, attcker.x], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
        }
        else//攻撃側が赤
        {
            //id > 0　&& id < 17 = 青駒
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, attcker.x] < 17 && CryPosition[attcker.y + 1, attcker.x] > 0)
            {
                targets.Add(CryPosition[attcker.y + 1, attcker.x]);
                crystat[CryPosition[attcker.y + 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, attcker.x], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, attcker.x] > 0 && CryPosition[attcker.y - 1, attcker.x] < 17)
            {
                targets.Add(CryPosition[attcker.y - 1, attcker.x]);
                crystat[CryPosition[attcker.y - 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, attcker.x], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
        }

        return flag;
    }

    
    public bool canBishopCheck(Crystal_Status attcker, int[,] CryPosition)
    {
        bool flag = false;
        if (attcker.iff)
        {
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }

            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
        }
        else
        {
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }

            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
        }

        return flag;
    }

   
    public bool canRookCheck(Crystal_Status attcker, int[,] CryPosition)
    {
        bool flag = false;
        if (attcker.iff)
        {
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, attcker.x] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, attcker.x]);
                crystat[CryPosition[attcker.y + 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, attcker.x], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, attcker.x] > 16)
            {
                targets.Add(CryPosition[attcker.y - 1, attcker.x]);
                crystat[CryPosition[attcker.y - 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, attcker.x], Quaternion.identity);
                flag = true;
            }
        }
        else
        {
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, attcker.x] < 17 && CryPosition[attcker.y + 1, attcker.x] > 0)
            {
                targets.Add(CryPosition[attcker.y + 1, attcker.x]);
                crystat[CryPosition[attcker.y + 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, attcker.x], Quaternion.identity);
                flag = true;
            }

            if (CryPosition[attcker.y, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (CryPosition[attcker.y, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, attcker.x] < 17 && CryPosition[attcker.y - 1, attcker.x] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, attcker.x]);
                crystat[CryPosition[attcker.y - 1, attcker.x]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, attcker.x], Quaternion.identity);
                flag = true;
            }
        }

        return flag;
    }

    public bool canPawnCheck(Crystal_Status attcker, int[,] CryPosition)
    {
        bool flag = false;
        if (attcker.iff)
        {
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 1) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y + 1 < 8 && CryPosition[attcker.y + 1, (attcker.x + 7) % 8] > 16)
            {
                targets.Add(CryPosition[attcker.y + 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y + 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y + 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
        }
        else
        {
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] < 17 && CryPosition[attcker.y - 1, (attcker.x + 1) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 1) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 1) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 1) % 8], Quaternion.identity);
                flag = true;
            }
            if (attcker.y - 1 >= 0 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] < 17 && CryPosition[attcker.y - 1, (attcker.x + 7) % 8] > 0)
            {
                targets.Add(CryPosition[attcker.y - 1, (attcker.x + 7) % 8]);
                crystat[CryPosition[attcker.y - 1, (attcker.x + 7) % 8]].targeted = true;

                Instantiate(Prefab_Effect, g_data.rawPosition[attcker.y - 1, (attcker.x + 7) % 8], Quaternion.identity);
                flag = true;
            }
        }
        return flag;
    }

    //攻撃駒に応じた攻撃可能化の判定
    //基本はこれを呼び出す(個別の呼び出しはしてない)
    public bool canAtkCheck(Crystal_Status attcker, int[,] CryPosition)
    {
        switch (attcker.tag)
        {
            case 0:
                return canKingCheck(attcker, CryPosition);
            case 1:
                //return canQueenCheck(attcker, CryPosition);
                return canKingCheck(attcker, CryPosition);
            case 2:
                return canBishopCheck(attcker, CryPosition);
            case 3:
                //return canKnightCheck(attcker, CryPosition);
                return canRookCheck(attcker, CryPosition);
            case 4:
                return canRookCheck(attcker, CryPosition);
            case 5:
                return canPawnCheck(attcker, CryPosition);
        }

        return false;
    }

    //攻撃対象になっていた各駒のtargeted をリセットしつつ、保持していたリストもリセット
    public void tListResetter()
    {
        for(int i = 0;i < targets.Count - 1;i++){
            crystat[targets[i]].targeted = false;            
        }
        targets.Clear();
    }
    
    //エフェクトの除去
    public void EffectRemover()
    {
        var clones = GameObject.FindGameObjectsWithTag("effect");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }





    //マーカー展開用スクリプト
    public void MrkMaker(int[,] MrkCheck)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (MrkCheck[i, j] == 1)
                {
                    GameObject tmp = (GameObject)Instantiate(marker, g_data.rawPosition[i, j], Quaternion.identity);
                    tmp.name = i.ToString() + j.ToString() + tmp.name;//マーカーに概念上の現在座標を記録
                }
            }
        }
    }

    //マーカー排除
    public void MrkRemover()
    {
        var clones = GameObject.FindGameObjectsWithTag("marker");//markerタグのついたオブジェクト(展開中の全オブジェクト)すべてを取得
        foreach (var clone in clones)//foreach文、わからないなら調べて
        {
            Destroy(clone);
        }
    }


    //マーカーを展開すべき位置の判定
    //二次元配列MrkCheck上のCryPositionと対応する位置を1にすることで判定
    //リストにも座標を保持(同じ要素数でアクセスすることでy,xをとれるようにしてる)
    public void MrkPawn(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        if (y < 7 && CryPosition[y + 1, x] == 0)
        {
            //右マス (y ,(x + 1)%8)  
            //左マス (y ,(x + 7)%8)　
            //右上マス (y + 1 ,(x + 1)%8)
            //左上マス (y + 1 ,(x + 7)%8)
            //上マス (y + 1 ,x)
            //右下マス (y - 1 ,(x + 1)%8)
            //左下マス (y - 1 ,(x + 7)%8)
            //下マス (y - 1 ,x)

            //リストへの保持
            movablex.Add(x);
            movabley.Add(y+1);
            //配列に保持
            MrkCheck[y + 1, x] = 1;
        }
    }

    public void eneMrkPawn(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        if (y > 0 && CryPosition[y - 1, x] == 0)
        {
            movablex.Add(x);
            movabley.Add(y-1);
            MrkCheck[y - 1, x] = 1;
        }
    }

    public void MrkQueen(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        int yc = y, xc = x;
        while (yc < 7 && CryPosition[yc + 1, xc] == 0 && MrkCheck[yc + 1, xc] == 0)
        {
            movablex.Add(xc);
            movabley.Add(yc+1);
            MrkCheck[yc + 1, xc] = 1;
            yc++;
        }
        yc = y;
        xc = x;

        while (yc > 0 && CryPosition[yc - 1, xc] == 0 && MrkCheck[yc - 1, xc] == 0)
        {
            movablex.Add(xc);
            movabley.Add(yc - 1);
            MrkCheck[yc - 1, xc] = 1;
            yc--;
        }
        yc = y;
        xc = x;

        while (CryPosition[yc, (xc + 7) % 8] == 0 && MrkCheck[yc, (xc + 7) % 8] == 0)
        {
            movablex.Add((xc + 7) % 8);
            movabley.Add(yc);
            MrkCheck[yc, (xc + 7) % 8] = 1;
            xc = (xc + 7) % 8;
        }
        yc = y;
        xc = x;

        while (CryPosition[yc, (xc + 1) % 8] == 0 && MrkCheck[yc, (xc + 1) % 8] == 0)
        {
            movablex.Add((xc + 1) % 8);
            movabley.Add(yc);
            MrkCheck[yc, (xc + 1) % 8] = 1;
            xc = (xc + 1) % 8;
        }

        yc = y;
        xc = x;

        while (yc < 7 && CryPosition[yc + 1, (xc + 7) % 8] == 0 && MrkCheck[yc + 1, (xc + 7) % 8] == 0)
        {
            movablex.Add((xc + 7) % 8);
            movabley.Add(yc+1);
            MrkCheck[yc + 1, (xc + 7) % 8] = 1;
            yc++;
            xc = (xc + 7) % 8;
        }
        yc = y;
        xc = x;

        while (yc < 7 && CryPosition[yc + 1, (xc + 1) % 8] == 0 && MrkCheck[yc + 1, (xc + 1) % 8] == 0)
        {
            movablex.Add((xc + 1) % 8);
            movabley.Add(yc+1);
            MrkCheck[yc + 1, (xc + 1) % 8] = 1;
            yc++;
            xc = (xc + 1) % 8;
        }
        yc = y;
        xc = x;

        while (yc > 0 && CryPosition[yc - 1, (xc + 1) % 8] == 0 && MrkCheck[yc - 1, (xc + 1) % 8] == 0)
        {
            movablex.Add((xc + 1) % 8);
            movabley.Add(yc-1);
            MrkCheck[yc - 1, (xc + 1) % 8] = 1;
            yc--;
            xc = (xc + 1) % 8;
        }
        yc = y;
        xc = x;

        while (yc > 0 && CryPosition[yc - 1, (xc + 7) % 8] == 0 && MrkCheck[yc - 1, (xc + 7) % 8] == 0)
        {
            movablex.Add((xc + 7) % 8);
            movabley.Add(yc - 1);
            MrkCheck[yc - 1, (xc + 7) % 8] = 1;
            yc--;
            xc = (xc + 7) % 8;
        }

    }



    public void MrkBishop(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        int yc = y, xc = x;
        while (yc < 7 && CryPosition[yc + 1, (xc + 7) % 8] == 0 && MrkCheck[yc + 1, (xc + 7) % 8] == 0)
        {
            movablex.Add((xc + 7) % 8);
            movabley.Add(yc+1);
            MrkCheck[yc + 1, (xc + 7) % 8] = 1;
            yc++;
            xc = (xc + 7) % 8;
        }
        yc = y;
        xc = x;

        while (yc < 7 && CryPosition[yc + 1, (xc + 1) % 8] == 0 && MrkCheck[yc + 1, (xc + 1) % 8] == 0)
        {
            movablex.Add((xc + 1) % 8);
            movabley.Add(yc+1);
            MrkCheck[yc + 1, (xc + 1) % 8] = 1;
            yc++;
            xc = (xc + 1) % 8;
        }
        yc = y;
        xc = x;

        while (yc > 0 && CryPosition[yc - 1, (xc + 1) % 8] == 0 && MrkCheck[yc - 1, (xc + 1) % 8] == 0)
        {
            movablex.Add((xc + 1) % 8);
            movabley.Add(yc-1);
            MrkCheck[yc - 1, (xc + 1) % 8] = 1;
            yc--;
            xc = (xc + 1) % 8;
        }
        yc = y;
        xc = x;

        while (yc > 0 && CryPosition[yc - 1, (xc + 7) % 8] == 0 && MrkCheck[yc - 1, (xc + 7) % 8] == 0)
        {
            movablex.Add((xc + 7) % 8);
            movabley.Add(yc - 1);
            MrkCheck[yc - 1, (xc + 7) % 8] = 1;
            yc--;
            xc = (xc + 7) % 8;
        }

    }



    public void MrkRook(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        int yc = y, xc = x;
        while (yc < 7 && CryPosition[yc + 1, xc] == 0 && MrkCheck[yc + 1, xc] == 0)
        {
            movablex.Add(xc);
            movabley.Add(yc + 1);
            MrkCheck[yc + 1, xc] = 1;
            yc++;
        }
        yc = y;
        xc = x;

        while (yc > 0 && CryPosition[yc - 1, xc] == 0 && MrkCheck[yc - 1, xc] == 0)
        {
            movablex.Add(xc);
            movabley.Add(yc -1);
            MrkCheck[yc - 1, xc] = 1;
            yc--;
        }
        yc = y;
        xc = x;

        while (CryPosition[yc, (xc + 7) % 8] == 0 && MrkCheck[yc, (xc + 7) % 8] == 0)
        {
            movablex.Add((xc + 7) % 8);
            movabley.Add(yc);
            MrkCheck[yc, (xc + 7) % 8] = 1;
            xc = (xc + 7) % 8;
        }
        yc = y;
        xc = x;

        while (CryPosition[yc, (xc + 1) % 8] == 0 && MrkCheck[yc, (xc + 1) % 8] == 0)
        {
            movablex.Add((xc + 1) % 8);
            movabley.Add(yc);
            MrkCheck[yc, (xc + 1) % 8] = 1;
            xc = (xc + 1) % 8;
        }
    }

    public void MrkKing(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        if (y < 7 && CryPosition[y + 1, x] == 0)
        {
            movablex.Add(x);
            movabley.Add(y + 1);
            MrkCheck[y + 1, x] = 1;
        }
        if (y < 7 && CryPosition[y + 1, (x + 1) % 8] == 0)
        {
            movablex.Add((x + 1) % 8);
            movabley.Add(y + 1);
            MrkCheck[y + 1, (x + 1) % 8] = 1;
        }
        if (y < 7 && CryPosition[y + 1, (x + 7) % 8] == 0)
        {
            movablex.Add((x + 7) % 8);
            movabley.Add(y + 1);
            MrkCheck[y + 1, (x + 7) % 8] = 1;
        }
        if (CryPosition[y, (x + 1) % 8] == 0)
        {
            movablex.Add((x + 1) % 8);
            movabley.Add(y);
            MrkCheck[y, (x + 1) % 8] = 1;
        }
        if (CryPosition[y, (x + 7) % 8] == 0)
        {
            movablex.Add((x + 7) % 8);
            movabley.Add(y);
            MrkCheck[y, (x + 7) % 8] = 1;
        }
        if (y > 0 && CryPosition[y - 1, x] == 0)
        {
            movablex.Add(x);
            movabley.Add(y - 1);
            MrkCheck[y - 1, x] = 1;
        }
        if (y > 0 && CryPosition[y - 1, (x + 1) % 8] == 0)
        {
            movablex.Add((x + 1) % 8);
            movabley.Add(y - 1);
            MrkCheck[y - 1, (x + 1) % 8] = 1;
        }
        if (y > 0 && CryPosition[y - 1, (x + 7) % 8] == 0)
        {
            movablex.Add((x + 7) % 8);
            movabley.Add(y - 1);
            MrkCheck[y - 1, (x + 7) % 8] = 1;
        }
    }

    public void MrkKnight(int[,] CryPosition, int[,] MrkCheck, int x, int y)
    {
        if (y + 2 < 8 && CryPosition[y + 2, (x + 1) % 8] == 0)
        {
            movablex.Add((x + 1) % 8);
            movabley.Add(y + 2);
            MrkCheck[y + 2, (x + 1) % 8] = 1;
        }

        if (y + 2 < 8 && CryPosition[y + 2, (x + 7) % 8] == 0)
        {
            movablex.Add((x + 7) % 8);
            movabley.Add(y + 2);
            MrkCheck[y + 2, (x + 7) % 8] = 1;
        }

        if (y + 1 < 8 && CryPosition[y + 1, (x + 2) % 8] == 0)
        {
            movablex.Add((x + 2) % 8);
            movabley.Add(y + 1);
            MrkCheck[y + 1, (x + 2) % 8] = 1;
        }

        if (y + 1 < 8 && CryPosition[y + 1, (x + 6) % 8] == 0)
        {
            movablex.Add((x + 6) % 8);
            movabley.Add(y + 1);
            MrkCheck[y + 1, (x + 6) % 8] = 1;
        }

        if (y > 1 && CryPosition[y - 2, (x + 1) % 8] == 0)
        {
            movablex.Add((x + 1) % 8);
            movabley.Add(y - 2);
            MrkCheck[y - 2, (x + 1) % 8] = 1;
        }

        if (y > 1 && CryPosition[y - 2, (x + 7) % 8] == 0)
        {
            movablex.Add((x + 7) % 8);
            movabley.Add(y - 2);
            MrkCheck[y - 2, (x + 7) % 8] = 1;
        }

        if (y > 0 && CryPosition[y - 1, (x + 2) % 8] == 0)
        {
            movablex.Add((x + 2) % 8);
            movabley.Add(y - 1);
            MrkCheck[y - 1, (x + 2) % 8] = 1;
        }

        if (y > 0 && CryPosition[y - 1, (x + 6) % 8] == 0)
        {
            movablex.Add((x + 6) % 8);
            movabley.Add(y - 1);
            MrkCheck[y - 1, (x + 6) % 8] = 1;
        }
    }


    //駒の種類に応じたマーカーの展開
    //個別呼び出しはせずこれを利用
    public void MrkGenerator(Crystal_Status crystat, int[,] CryPosition, bool pvp, bool eturn)
    {
        int[,] MrkCheck = new int[8, 8];

        switch (crystat.tag)
        {
            case 0:
                MrkKing(CryPosition, MrkCheck, crystat.x, crystat.y);
                break;
            case 1:
                MrkQueen(CryPosition, MrkCheck, crystat.x, crystat.y);
                break;
            case 2:
                MrkBishop(CryPosition, MrkCheck, crystat.x, crystat.y);
                break;
            case 3:
                MrkKnight(CryPosition, MrkCheck, crystat.x, crystat.y);
                break;
            case 4:
                MrkRook(CryPosition, MrkCheck, crystat.x, crystat.y);
                break;
            case 5:
                if (crystat.iff)
                {
                    MrkPawn(CryPosition, MrkCheck, crystat.x, crystat.y);
                }
                else
                {
                    eneMrkPawn(CryPosition, MrkCheck, crystat.x, crystat.y);
                }
                break;
        }

        if (pvp || !eturn) {
            MrkMaker(MrkCheck);
        }
    }

    //移動可能な座標のリストのリセット
    //このリストはCOMしかいらないので人が操作するターンなら判定直後に実行しちゃってOK
    public void mListResetter()
    {        
        movablex.Clear();
        movabley.Clear();
    }
    
}
 