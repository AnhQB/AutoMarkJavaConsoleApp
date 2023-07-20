//your job is to complete this class
public class Cake{ 
    String name; 
    double price; 
    double itax;
    
    public Cake(String name, double price, double itax) { 
         //your code goes here     
         this.name = name;
         this.price = price;
         this.itax = itax;
    }
    //add and complete you other methods (if needed) here   
    public double getSalesPrice(){
        return this.price + this.itax;
    }
}
