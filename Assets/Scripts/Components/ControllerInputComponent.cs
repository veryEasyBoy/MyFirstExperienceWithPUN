
public class ControllerInputComponent
{
	private float moveVertical;
	private float moveHorizontal;

	private PlayerInputActions playerInput;

	public float MoveVertical => moveVertical;
	public float MoveHorizontal => moveHorizontal;

	public void OnEnable()
	{
		playerInput = new PlayerInputActions();

		playerInput.Player.Forward.performed += ctx => moveVertical = ctx.ReadValue<float>();
		playerInput.Player.Forward.canceled += ctx => moveVertical = 0f;

		playerInput.Player.Backward.performed += ctx => moveVertical -= ctx.ReadValue<float>();
		playerInput.Player.Backward.canceled += ctx => moveVertical = 0f;

		playerInput.Player.Rightward.performed += ctx => moveHorizontal = ctx.ReadValue<float>();
		playerInput.Player.Rightward.canceled += ctx => moveHorizontal = 0f;

		playerInput.Player.Leftward.performed += ctx => moveHorizontal -= ctx.ReadValue<float>();
		playerInput.Player.Leftward.canceled += ctx => moveHorizontal = 0f;

		playerInput.Enable();
	}
}
