using UnityEngine;

public class SampleRotation : BaseCommandComponent<RotateCommand> {

    private float startAngle = 0f;

    private float targetAngle = 0f;

    private Vector3 angleAxis;

    public void Execute(float StartAngle, float TargetAngle, Vector3 AngleAxis) {
        startAngle = StartAngle;

        targetAngle = TargetAngle;

        angleAxis = AngleAxis;

        Execute();
    }

    protected override void SetUpCommand(RotateCommand Command) {
        Command.SetUpCommand(transform, startAngle, targetAngle, angleAxis);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.A)) {
            var startAngle = transform.localEulerAngles.z;

            Execute(startAngle, startAngle + 10f, Vector3.forward);
        } else if(Input.GetKeyDown(KeyCode.D)) {
            Undo();
        }
    }

    private void Start() {
        Init();
    }
}
