using System;
using System.Linq;
					
public class Program
{
	
	public static string R() /* Fonction de récup de string */
	{
		return Console.ReadLine();
	}
	
	public static int Ri() /* Fonction de récup de int */
	{
		return int.Parse(Console.ReadLine());
	}
	
	public static void P(string a) /* Procédure d'affichage pour string */
	{
		Console.WriteLine(a);
	}
	
	public static void Pi(int a) /* Procédure d'affichage pour int */
	{
		Console.WriteLine(a);
	}
	
	public static int verif_cidr(int cidr) /* Vérification du CIDR */
	{
		while (cidr <= 0 || cidr > 31) /* Tant que c'est trop grand ou trop petit, on recommence */
		{
			if (cidr <= 0)
			{
				P("Ce nombre est trop petit. Réessayez");
			}
			
			else if (cidr > 31)
			{
				P("Ce nombre est trop grand. Réessayez");
			}
			cidr = Ri(); /* Récupération de la valeur du CIDR */
		}
		return cidr;
	}
	
	public static int verif_octet(int a) /* Vérification de la taille de chaque octet */
	{
		while (a > 255 || a < 0) /* Tant que c'est trop grand ou trop petit, on recommence */
		{
			if (a < 0)
			{
				P("Ce nombre est trop petit. Réessayez");
			}
			
			else if (a > 255)
			{
				P("Ce nombre est trop grand. Réessayez");
			}
			a = Ri();
		}
		return a;
	}
	
	public static void broadcast_network(string IP, string message) /* Conversion dé binaire à décimal d'une IP */
	{
		string IP1 = IP.Substring(0, 8); /* On découpe le long string en 4 octets (position 0, 8 chars d'après sont pris) */
		string IP2 = IP.Substring(8, 8); 
		string IP3 = IP.Substring(16, 8);
		string IP4 = IP.Substring(24, 8);
		
		byte IPP1 = Convert.ToByte(IP1, 2); /* Conversion de chaque octet de binaire à décimal */
		byte IPP2 = Convert.ToByte(IP2, 2);
		byte IPP3 = Convert.ToByte(IP3, 2);
		byte IPP4 = Convert.ToByte(IP4, 2);
		
		P(message+" est : " + IPP1 +"."+IPP2+"."+IPP3+"."+IPP4); /* Message + Affichage d'une IP */
	}
	
	public static void masque(int cidr) /* Calcul du masque de sous-réseau */
	{
		int octets_hote = 32 - cidr; /* On obtient les octets hôtes */
		int octets_reseau = 32 - octets_hote; /* On obtient les octets réseau */
		string partie_network = string.Concat(Enumerable.Repeat("1", octets_reseau)); /* Tous les octets réseau à 1 */
		string partie_hote = string.Concat(Enumerable.Repeat("0", octets_hote)); /* Tous les octets hôte à 0 */
		string masque_binaire = partie_network + partie_hote; /* Le masque entièrement en binaire */
		
		string masque1 = masque_binaire.Substring(0, 8); /* On découpe le long string en 4 octets (position 0, 8 chars d'après sont pris) */
		string masque2 = masque_binaire.Substring(8, 8); 
		string masque3 = masque_binaire.Substring(16, 8);
		string masque4 = masque_binaire.Substring(24, 8);
		
		byte octet1 = Convert.ToByte(masque1, 2); /* Conversion de chaque octet de binaire à décimal */
		byte octet2 = Convert.ToByte(masque2, 2);
		byte octet3 = Convert.ToByte(masque3, 2);
		byte octet4 = Convert.ToByte(masque4, 2);
		
		string masque = octet1 + "." + octet2 + "." + octet3 + "." + octet4;
		P("Masque /" + cidr + " <=> " + masque);
	}
		
	public static void verif_host(int un, int deux, int trois, int quatre, int cidr) /* Cette procédure va vérifier la partie hôte de l'IP */
	{
		P("Votre IP et masque : " + un + "." + deux + "." + trois + "." + quatre); /* Information sur l'IP et masque*/
		masque(cidr); /* Affichage du masque en notation classique et en notation CIDR */
		int octets_hote = 32 - cidr; /* On obtient les octets hôtes */
		string bin_un = Convert.ToString(un, 2); /* On convertit chaque octet en string */
		string bin_deux = Convert.ToString(deux, 2);
		string bin_trois = Convert.ToString(trois, 2);
		string bin_quatre = Convert.ToString(quatre, 2);
		string IP_binaire = bin_un.PadLeft(8, '0') + bin_deux.PadLeft(8, '0') + bin_trois.PadLeft(8, '0') + bin_quatre.PadLeft(8, '0'); /* IP totalement en binaire de 32 octets */
		string binaire_hote = IP_binaire.Substring(IP_binaire.Length - octets_hote); /* On supprime la partie réseau */
		string que_des_1 = string.Concat(Enumerable.Repeat("1", octets_hote)); /* C'est l'adresse broadcast */
		string que_des_0 = string.Concat(Enumerable.Repeat("0", octets_hote)); /* C'est l'adresse réseau */
		
		if (binaire_hote == que_des_1 || binaire_hote == que_des_0)  /* Si l'IP et le masque correspondent à une IP réseau ou diffusion */
		{
			P("Données invalides. Veuillez retaper votre adresse IP");
		}

		else
		{
			string binaire_reseau = IP_binaire.Remove(IP_binaire.Length - octets_hote); /* On prend la partie réseau en binaire */
			
			string binaire_IP_network = binaire_reseau + que_des_0; /* IP en binaire de l'adresse réseau */
			string binaire_IP_broadcast = binaire_reseau + que_des_1; /* IP en binaire de l'adresse diffusion */
			
			string pas_que_des_0 = string.Concat(Enumerable.Repeat("0", (octets_hote-1)));
			string pas_que_des_1 = string.Concat(Enumerable.Repeat("1", (octets_hote-1)));
			string hote_premiere = pas_que_des_0 + "1"; /* Binaire hôte de la première IP possible */
			string hote_derniere = pas_que_des_1 + "0"; /* Binaire hôte de la dernière IP possible */
			string premiere = binaire_reseau + hote_premiere; /* Binaire IP de la première IP possible */
			string derniere = binaire_reseau + hote_derniere; /* Binaire IP de la dernière IP possible */
			
			broadcast_network(binaire_IP_network, "L'adresse de réseau"); /* Affichage de l'@ réseau */
			broadcast_network(binaire_IP_broadcast, "L'adresse de broadcast"); /* Affichage de l'@ diffusion */
			broadcast_network(premiere, "La première IP disponible"); /* Affichage de la première @ */
			broadcast_network(derniere, "La dernière IP disponible"); /* Affichage de la dernière @ */
			string hotes_plus_deux = string.Concat(Enumerable.Repeat("1", (octets_hote-2)));
			string binaire_hotes = hotes_plus_deux + "10";
			string nombre_hotes = Convert.ToInt32(binaire_hotes, 2).ToString();  /* Conversion du nombre d'hôtes en décimal en string*/
			P("Dans ce réseau, il peut y avoir " + nombre_hotes + " hôtes possibles"); /* Affichage du nombre d'hôtes possibles */
		}
	}
	
	
	public static void Main()
	{
		P("Saisissez votre adresse IP (4 valeurs demandées)"); /* Demande de l'IP */
		int un = Ri();  /* Récupération de chaque octet */
		verif_octet(un); /* Vérifié s'il est < 0 et > 255 */
		
		int deux = Ri();
		verif_octet(deux);
		
		int trois = Ri();
		verif_octet(trois);
		
		int quatre = Ri();
		verif_octet(quatre);
					
		P("Saisissez le masque en notation CIDR (1-31)");
		int cidr = Ri(); /* Récupération du masque en notation CIDR */
		verif_cidr(cidr); /* Vérifié s'il est < 0 et > 32 */
		verif_host(un, deux, trois, quatre, cidr); /* Vérification si l'IP est possible et affi */
	}
}