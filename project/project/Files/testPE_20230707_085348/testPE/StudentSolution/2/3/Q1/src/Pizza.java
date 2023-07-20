
public class Pizza {
    private double diameter;
    private int slice;
    
    public Pizza(double diameter, int slice) {
        //your code goes here 
        this.diameter = diameter;
        this.slice = slice;
    }    
   //add and complete your other methods here (if needed)

    @Override
    public String toString() {
        return this.diameter + "\t" + this.slice;
    }
     
}
