using UnityEngine;

//ガベージコレクションを発生させないコマンドパターンの利用
//動的にコマンド追加できない代わりに、動的にメモリ確保もしない
public class SampleMover : BaseCommandComponent<MoveCommand> {

    private bool useLocal = false;

    private Vector3 targetPos = Vector3.zero;

    //実行処理
    public void Execute(Vector3 TargetPos, bool UseLocal) {
        targetPos = TargetPos;

        useLocal = UseLocal;

        Execute();
    }

    //実行コマンドに必要のパラメーターを設定する
    protected override void SetUpCommand(MoveCommand Command) {
        Command.SetUpCommand(targetPos, transform, useLocal);
    }

#if UNITY_EDITOR
    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)) {
            var pos = transform.position;

            pos.x += Random.Range(1, 5);

            Execute(pos, false);
        } else if(Input.GetKeyDown(KeyCode.D)) {
            Undo();
        }
    }
#endif

    private void Start() {
        Init();
    }
}