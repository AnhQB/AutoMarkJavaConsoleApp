
import java.util.ArrayList;

/*
 * DO NOT EDIT THIS FILE
 */
public interface ICake {   
    /*
     * return the name of Cake which has highest sales price
     */
    public String getHighestPrice(ArrayList a);        
    /*
     * Count and return the number of the Cake that have imported tax  
     * less than the imported tax of the first Cake in the list
     */
    public int count(ArrayList a);
}
