
import java.util.ArrayList;
//Your job is to complete this class 
//this class will implement IFlower interface
public class MyCake implements ICake
{

    //write the definition of method getHighestPrice here 
    @Override
    public String getHighestPrice(ArrayList a) {
         String out = "";   
         int index = 0;
         for(int i = 1; i < a.size(); i++){
             double maxTemp = Cake.class.cast(a.get(index)).getSalesPrice();
             double current = Cake.class.cast(a.get(i)).getSalesPrice();
             if(current > maxTemp){
                 index = i;
             }
         }
         out = Cake.class.cast(a.get(index)).name;
         return out;
    }
 
   
    //write the definition of method count here 
    @Override
    public int count(ArrayList a) {
        int out = -1;
        //your codes goes here
        out++;
        for(int i = 1; i < a.size(); i++){
             double first = Cake.class.cast(a.get(0)).getSalesPrice();
             double current = Cake.class.cast(a.get(i)).getSalesPrice();
             if(current < first){
                 out++;
             }
         }
        return out;
    }
    
    //add and complete you other methods (if needed) here   
     
}
