// ======= DO NOT EDIT THIS FILE ============
import java.io.*;
class Main
{
   public static void main(String args[]) throws Exception
   {
       BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
       System.out.print("Enter added toppings: ");
       String addedToppings = in.readLine();
       System.out.print("Enter diameter: ");
       double diamter = Double.parseDouble(in.readLine());
       System.out.print("Enter number of slices: ");
       int slices = Integer.parseInt(in.readLine());
       System.out.println("OUTPUT:");
       Pizza p = new Pizza(diamter, slices);
       System.out.println(p);
       p = new DuluxePizza(addedToppings, diamter, slices);
       System.out.println(p);
   }
 }
