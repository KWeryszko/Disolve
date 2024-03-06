using Godot;


public partial class Hp : Node2D
{
	

		[Signal]
		public delegate void HealthChangedEventHandler(HealthUpdate healthUpdate);
		[Signal]
		public delegate void DiedEventHandler();
		[Export]
		public float MaxHealth
		{
			get => maxHealth;
			private set{
				maxHealth = value;
				if(CurrentHealth>maxHealth)
				{
					CurrentHealth = maxHealth;
				}
			}
		}
	
	public bool HasHealthRemaining => !Mathf.IsEqualApprox(CurrentHealth, 0f);
	public float CurrentHealth
	{
		get=> CurrentHealth;
		private set
		{
			var previousHealth= currentHealth;
			currentHealth = Mathf.Clamp(value, 0, MaxHealth);
			var healthUpdate=new HealthUpdate
			{
				PreviousHealth = previousHealth,
				CurrentHealth = currentHealth,
				MaxHealth = MaxHealth,
				IsHeal = previousHealth <= currentHealth
			};
			EmitSignal(SignalName.HealthChanged, healthUpdate);
			if(!HasHealthRemaining && !hasDied)
			{
				hasDied = true;
				EmitSignal(SignalName.Died);

			}
		}
	}
	
	public bool IsDamaged => CurrentHealth < MaxHealth;
	private bool hasDied;
	public float maxHealth=10;
	public float currentHealth=10;
	
   /* public override void _Notification(long what)
    {
        if(what == NotificationSceneInstantiated)
		{
			this.WireNodes();
		}
    }
	*/
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	CallDeferred(nameof(InitializeHealth));

	}
	public void Damage(float damage)
	{
		CurrentHealth -= damage;
		
	}
	public void Heal(float heal)
	{
		Damage(-heal);
	}
	public void SetMaxHealth()
	{
		CurrentHealth = MaxHealth;
	}
	private void InitializeHealth()
	{
		CurrentHealth= MaxHealth;
	}
	public void ApplyScaling(Curve curve, float progress)
	{
		CallDeferred(nameof(ApplyScalingInternal), curve, progress);
	}
	private void ApplyScalingInternal(Curve curve, float progress)
	{
		var curveValue = curve.Sample(progress);
		MaxHealth += 1f + curveValue;
		CurrentHealth= MaxHealth;

	}
	public partial class HealthUpdate : RefCounted
	{
		public float PreviousHealth;
		public float CurrentHealth;
		public float MaxHealth;
		public float HealthPercent;
		public bool IsHeal;
	}
}
