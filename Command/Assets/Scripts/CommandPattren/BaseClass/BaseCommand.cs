//すべてのコマンドの親
public class BaseCommand {
    //現在戻せる状態なら、true
    public bool CanUndo {
        get;
        private set;
    }

    public virtual void Execute() {
        CanUndo = true;
    }

    public virtual void Undo() {
        CanUndo = false;
    }
}