using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    public RectTransform healthBar; // Referencia al RectTransform de la barra de vida

    public float maxHealth = 100f;
    public float currentHealth = 100f;

    // Valores de la barra llena
    private Vector3 fullPosition = new Vector3(0, 0, 0);
    private float fullWidth = 1.98f;
    private float fullHeight = 0.09f;

    // Valores de la barra vacía
    private Vector3 emptyPosition = new Vector3(-0.989690006f, -0.00645000022f, 0);
    private float emptyWidth = 0.00042f;
    private float emptyHeight = 0.1029f;

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // Calcula el porcentaje de vida actual
        float healthPercentage = currentHealth / maxHealth;

        // Interpola entre los valores de la barra llena y vacía
        healthBar.localPosition = Vector3.Lerp(emptyPosition, fullPosition, healthPercentage);
        healthBar.sizeDelta = new Vector2(
            Mathf.Lerp(emptyWidth, fullWidth, healthPercentage),
            Mathf.Lerp(emptyHeight, fullHeight, healthPercentage)
        );
    }

    // Método para actualizar la vida actual
    public void SetHealth(float health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);
    }
}
