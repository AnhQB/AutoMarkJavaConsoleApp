
public class DuluxePizza extends Pizza{
    private String addedTopping;
    private double diameter;
    private int slice;
    
    public DuluxePizza(String addedTopping, double diameter, int slice) { 
        super(diameter, slice);
        //your code goes here
        this.addedTopping = addedTopping;
    }    
    //add and complete your other methods here (if needed)

    @Override
    public String toString() {
        return addedTopping + "\t" + super.toString();
    }
 
}
