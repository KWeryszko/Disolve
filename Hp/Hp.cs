using Godot;
public partial class Hp : Node2D
{
	

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
		
		}
	}
	
	public bool IsDamaged => CurrentHealth < MaxHealth;
	private bool hasDied;
	public float maxHealth=10;
	public float currentHealth=10;
	
 
    public override void _Ready()
	{
		CallDeferred(nameof(InitializeHealth));

    }
	public void Damage(float damage)
	{
		currentHealth -= damage;
	}
	public void Heal(float heal)
	{
		Damage(-heal);
	}
	
	private void InitializeHealth()
	{
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
