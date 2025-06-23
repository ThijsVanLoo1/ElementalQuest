using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOpacity : MonoBehaviour
{
    public GameObject hiddenInventory;
    public GameObject VisibleInventory;

    public static GameObject ore;
    private string oreName;
    public Transform items;

    //retrieve all images/elements
    public Image Image_H;
    public Image Image_Li;
    public Image Image_Be;
    public Image Image_Na;
    public Image Image_Mg;
    public Image Image_K;
    public Image Image_Ca;
    public Image Image_Rb;
    public Image Image_Sr;
    public Image Image_Sc;
    public Image Image_Y;
    public Image Image_Ti;
    public Image Image_Zr;
    public Image Image_V;
    public Image Image_Nb;
    public Image Image_Cr;
    public Image Image_Mo;
    public Image Image_Mn;
    public Image Image_Tc;
    public Image Image_Fe;
    public Image Image_Ru;
    public Image Image_Co;
    public Image Image_Rh;
    public Image Image_Ni;
    public Image Image_Pd;
    public Image Image_Cu;
    public Image Image_Ag;
    public Image Image_Zn;
    public Image Image_Cd;
    public Image Image_He;
    public Image Image_B;
    public Image Image_C;
    public Image Image_N;
    public Image Image_O;
    public Image Image_F;
    public Image Image_Ne;
    public Image Image_Al;
    public Image Image_Si;
    public Image Image_P;
    public Image Image_S;
    public Image Image_Cl;
    public Image Image_Ar;
    public Image Image_Ga;
    public Image Image_Ge;
    public Image Image_As;
    public Image Image_Se;
    public Image Image_Br;
    public Image Image_Kr;
    public Image Image_In;
    public Image Image_Sn;
    public Image Image_Sb;
    public Image Image_Te;
    public Image Image_I;
    public Image Image_Xe;


    // Set opacity (0 = transparent, 1 = opaque)
    public void SetOpacity(Image elementName)
    {
        Color color = elementName.color;
        color.a = Mathf.Clamp01(1f); // Alpha between 1 and 0
        elementName.color = color;
    }

    private void Update()
    {
        if (Input.GetButtonDown("OpenInventory"))
        {
            ToggleCanvas();
        }
    }

    // Optionally: Toggle visibility
    public void ToggleCanvas()
    {
        hiddenInventory.SetActive(!hiddenInventory.activeSelf);
        VisibleInventory.SetActive(!hiddenInventory.activeSelf);
    }

    public void SendToInventory()
    {
        oreName = ore.GetComponent<Ore>().oreName;
    }

    public void AddToInventory(string name)
    {
        oreName = name;
        Image elementImage = GetImageByOreName(oreName);
        if (elementImage != null)
        {
            SetOpacity(elementImage);
        }
    }

    //in deze functie doorsturen naar crafting voor luuk, vraag om verduidelijking als nodig :)
    private Image GetImageByOreName(string name)
    {
        Debug.Log(name);
        switch (name)
        {
            case "Hydrogen":
                items.Find("hydrogen").gameObject.SetActive(true);
                return Image_H;
            case "lithium":
                return Image_Li;
            case "Berium":
                return Image_Be;
            case "Sodium":
                items.Find("sodium").gameObject.SetActive(true);
                return Image_Na;
            case "Magnesium":
                return Image_Mg;
            case "Kalium":
                return Image_K;
            case "Calcium":
                return Image_Ca;
            case "Rubidium":
                return Image_Rb;
            case "Strontium":
                return Image_Sr;
            case "Scandium":
                return Image_Sc;
            case "Yttrium":
                return Image_Y;
            case "Titanium":
                return Image_Ti;
            case "Zirkonium":
                return Image_Zr;
            case "Vanadium":
                return Image_V;
            case "Niobium":
                return Image_Nb;
            case "Chroom":
                return Image_Cr;
            case "Molydeen":
                return Image_Mo;
            case "Mangaan":
                return Image_Mn;
            case "Technetium":
                return Image_Tc;
            case "Iron":
                return Image_Fe;
            case "Ruthenium":
                return Image_Ru;
            case "Cobalt":
                return Image_Co;
            case "Rhodium":
                return Image_Rh;
            case "Nikkel":
                return Image_Ni;
            case "Palladium":
                return Image_Pd;
            case "Copper":
                return Image_Cu;
            case "Silver":
                return Image_Ag;
            case "Zink":
                return Image_Zn;
            case "Cadium":
                return Image_Cd;
            case "Helium":
                return Image_He;
            case "Barium":
                return Image_B;
            case "Carbon":
                items.Find("carbon").gameObject.SetActive(true);
                return Image_C;
            case "Nitrogen":
                items.Find("nitrogen").gameObject.SetActive(true);
                return Image_N;
            case "Oxygen":
                items.Find("oxygen").gameObject.SetActive(true);
                return Image_O;
            case "Fluor":
                return Image_F;
            case "Neon":
                return Image_Ne;
            case "Aluminium":
                return Image_Al;
            case "Sicilium":
                return Image_Si;
            case "Fosfor":
                return Image_P;
            case "Zwavel":
                return Image_S;
            case "Chlorine":
                items.Find("chlorine").gameObject.SetActive(true);
                return Image_Cl;
            case "Argon":
                return Image_Ar;
            case "Gallium":
                return Image_Ga;
            case "Germanium":
                return Image_Ge;
            case "Arseen":
                return Image_As;
            case "Selenium":
                return Image_Se;
            case "Broom":
                return Image_Br;
            case "Krypton":
                return Image_Kr;
            case "Indium":
                return Image_In;
            case "Tin":
                return Image_Sn;
            case "Antimoon":
                return Image_Sb;
            case "Telluur":
                return Image_Te;
            case "Jood":
                return Image_I;
            case "Xenon":
                return Image_Xe;
            default:
                return null;
        }
    }
}
