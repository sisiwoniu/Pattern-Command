using UnityEngine;

//回転用コマンド
public class RotateCommand : BaseCommand {

    private Transform rotateTarget;

    private Quaternion startQuaternion;

    private float targetAngle;

    private float startAngle;

    private Vector3 rotateAxis;

    public override void Execute() {
        rotateTarget.transform.rotation = Quaternion.AngleAxis(targetAngle, rotateAxis);

        base.Execute();
    }

    public override void Undo() {
        rotateTarget.transform.rotation = Quaternion.AngleAxis(startAngle, rotateAxis);

        base.Undo();
    }

    //コマンドセットアップ
    public void SetUpCommand(Transform Target, float StartAngle, float TargetAngle, Vector3 RotateAxis) {
        rotateTarget = Target;

        rotateAxis = RotateAxis;

        targetAngle = TargetAngle;

        startAngle = StartAngle;
    }
}