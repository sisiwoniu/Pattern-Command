using System.Collections.Generic;
using UnityEngine;

//コマンドを管理するクラス、ここでコマンドバッファを管理していて、コマンドの実行処理や戻す処理などを呼び出す
//現状は動的にコマンドバッファを拡張するのがなしと想定する。よって動的確保しない、動的解放もしない、GCなし
//大量のインスタンスで使うことを避けるように使ってください
//設定した最大バッファを超えるようにコマンドを実行する場合、一番先頭からコマンドを上書きし、利用する
//例えば「MaxCommandCount = 10」の場合、「11個目」を利用しようとすると、インデックスを先頭からもう一回回すようになる
public abstract class BaseCommandComponent<T> : MonoBehaviour where T : BaseCommand, new() {

    //コマンドバッファの最大値
    [SerializeField, Range(0, 100)]
    private int MaxCommandCount = 10;

    //コマンドインスタンステーブル
    private Dictionary<int, T> usingCommandDict = new Dictionary<int, T>();

    //使用中のコマンドインスタンス
    private int usingIndex = 1;

    //実行したコマンドを戻す
    //この関数は特にオーバーライドなど考えなくてもいい、直接呼び出すにも問題ない
    public void Undo() {
        //ここもインデックス回し
        var index = usingIndex - 1 < 1 ? MaxCommandCount : usingIndex - 1;

        var command = usingCommandDict[index];

        if(command.CanUndo) {
            command.Undo();

            usingIndex = index;

            Debug.Log("戻したコマンドのインデックス: " + usingIndex);
        }
    }

    //コマンドを実行
    //実行前の段階で、コマンドの種類に合わせて実行必要のデータをセットする必要があるので
    //継承した子クラス達で状況に合わせて実装してください
    protected void Execute() {
        var command = usingCommandDict[usingIndex];

        SetUpCommand(command);

        command.Execute();

        Debug.Log("実行したコマンドのインデックス: " + usingIndex);

        //ここでインデックスを回している
        usingIndex = usingIndex + 1 > MaxCommandCount ? 1 : usingIndex + 1;
    }

    //コマンドバッファを初期化
    protected void Init() {
        if(usingCommandDict.Count == 0) {
            for(int i = 0; i < MaxCommandCount; i++) {
                usingCommandDict.Add(i + 1, new T());
            }
        }
    }

    //子クラスの振る舞いに合わせて実装してください
    protected abstract void SetUpCommand(T Command);

    //削除されるタイミングのイベント
    private void OnDestroy() {
        usingCommandDict.Clear();
    }
}