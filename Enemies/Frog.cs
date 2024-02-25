using Godot;
using System;

public partial class Frog : CharacterBody2D
{
	public const float Speed = 80.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	// private Node2D player = GetNode("EnemySound");
	
	bool chase = false;

	public override void _Ready()
	{
		
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		CharacterBody2D player = GetNode<CharacterBody2D>("../Player");
		AnimatedSprite2D enemyAnimatedSprite = GetNode<AnimatedSprite2D>("../Frog/AnimatedSprite2D");

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		if (chase == true){
			Vector2 direction = player.Position - this.Position;
			//GD.Print(direction);
			if (direction.X > 0){
				//GD.Print("chase right");
				velocity.X = Speed;
				enemyAnimatedSprite.FlipH = true;
			} else {
				//GD.Print("left");
				velocity.X = -Speed;
				enemyAnimatedSprite.FlipH = false;	
			}
		}
		if (chase == false){
			velocity.X = 0;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void _on_player_detection_body_entered(Node2D body){
		
		if (body.Name == "Player"){
			chase = true;
		}
	}

	public void _on_player_detection_body_exited(Node2D body){
		if (body.Name == "Player"){
			chase = false;
		}
	}
}