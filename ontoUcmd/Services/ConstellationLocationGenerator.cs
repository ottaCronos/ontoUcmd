namespace ontoUcmd.Services
{
    
    public class ConstellationLocationGenerator {
    
        public  string crlf = "\n";
        
        public  string locationFile = "D:\\xampp\\htdocs\\ontoU\\dive\\data\\locations1.txt";
        
        public  string locationJsonFile = "D:\\xampp\\htdocs\\ontoU\\dive\\data\\constellation-node-locations_.json";
        
        public  string[] allType;
        
        public  string[] Element;
        
        public  string[] Country;
        
        public  string[] Region;
        
        public  string[] Project;
        
        public  string[] People;
        
        public  string[] Publication;
        
        class PlacedItem {
            
            public string name = "";
            
            public int dim = 0;
            
            public int x = 0;
            
            public int y = 0;
            
            public double dist(PlacedItem pi) {
                return Math.sqrt((((this.x - pi.x) 
                                * (this.x - pi.x)) 
                                + ((this.y - pi.y) 
                                * (this.y - pi.y))));
            }
            
            public final String toString(int com, int degre) {
                return "{\"id\":\"\"+name+";
                (",\"community\":" 
                            + (com + (",\"degree\":" 
                            + (degre + (",\"x_fixed\":" 
                            + (this.x + (",\"y_fixed\":" 
                            + (this.y + "}"))))))));
            }
        }
        
        public ArrayList<PlacedItem> allConcept = new ArrayList<PlacedItem>();
        
        public ArrayList<PlacedItem> allElement = new ArrayList<PlacedItem>();
        
        public ArrayList<PlacedItem> allCountry = new ArrayList<PlacedItem>();
        
        public ArrayList<PlacedItem> allRegion = new ArrayList<PlacedItem>();
        
        public ArrayList<PlacedItem> allProject = new ArrayList<PlacedItem>();
        
        public ArrayList<PlacedItem> allPeople = new ArrayList<PlacedItem>();
        
        public ArrayList<PlacedItem> allPublication = new ArrayList<PlacedItem>();
        
        public ArrayList[] allTypeRead;
        
        public ArrayList[] allElement;
        
        public ArrayList[] allCountry;
        
        public ArrayList[] allRegion;
        
        public ArrayList[] allProject;
        
        public ArrayList[] allPeople;
        
        public ArrayList[] allPublication;
        
        public static void fillAllItem() {
            try {
                BufferedReader;
            }
            
            br = new BufferedReader(new FileReader(this.locationFile));
            int iLine = 0;
            String line;
            while ((br.readLine() != null)) {
                if (line.contains(":")) {
                    String concept = line.substring((line.indexOf(".") + 1), line.indexOf(":"));
                    String dimS = line.substring((line.indexOf(":") + 1), line.indexOf(";"));
                    int dim = Integer.parseInt(dimS);
                    PlacedItem c = new PlacedItem();
                    c.name = concept;
                    c.dim = 100;
                    this.affectPlace(c);
                    this.allConcept.add(c);
                }
                else if (line.contains(".")) {
                    String lu = line.substring(0, line.indexOf("."));
                    String name = line.substring((line.indexOf(".") + 1), line.indexOf(";"));
                    System.out.println(("item  " 
                                    + (lu + ("  " + name))));
                    for (int i = 1; (i < this.allTypeRead.length); i++) {
                        if (lu.equalsIgnoreCase(this.allType[i])) {
                            PlacedItem pi = new PlacedItem();
                            pi.name = name;
                            this.affectPlace(pi);
                            this.allTypeRead[i].add(pi);
                        }
                        
                    }
                    
                }
                
            }
            
            IOException;
            ioe;
            //  ...
        }
        
        public static void affectPlace(PlacedItem pi) {
            boolean found = false;
            do {
                pi.x = ArpenteurGeometryTools.getIntRandom(-900, 900);
                pi.y = ArpenteurGeometryTools.getIntRandom(-900, 900);
                found = this.roomOk(pi);
            } while (!found);
            
        }
        
        public static bool roomOk(PlacedItem pi) {
            bool ok = true;
            for (int i = 0; (i < this.allTypeRead.Length); i++) {
                for (int j = 0; (j < this.allTypeRead[i].size()); j++) {
                    PlacedItem pj = ((PlacedItem)(this.allTypeRead[i].get(j)));
                    if ((pi.dist(pj) < 100)) {
                        return false;
                    }
                }
            }
            
            return ok;
        }
        
        public final void writePlace() {
            try {
                PrintWriter fo = new PrintWriter(new FileOutputStream(new File(this.locationJsonFile)));
                fo.print(("[" + this.crlf));
                for (int i = 0; (i < this.allTypeRead.length); i++) {
                    for (int j = 0; (j < this.allTypeRead[i].size()); j++) {
                        PlacedItem pj = ((PlacedItem)(this.allTypeRead[i].get(j)));
                        if (((i 
                                    == (this.allTypeRead.length - 1)) 
                                    && (j 
                                    == (this.allTypeRead[i].size() - 1)))) {
                            fo.print((pj.toString((i + 1), 150) + this.crlf));
                        }
                        else {
                            fo.print((pj.toString((i + 1), 150) + ("," + this.crlf)));
                        }
                        
                    }
                    
                }
                
                fo.print(("]" + this.crlf));
                fo.close();
            }
            catch (Exception e) {
                
            }
            
        }
        
        public final void showRead() {
            System.out.println(("Concept " + this.allConcept.size()));
            System.out.println(("Element " + this.allElement.size()));
            System.out.println(("Country " + this.allCountry.size()));
            System.out.println(("Region  " + this.allRegion.size()));
            System.out.println(("Project " + this.allProject.size()));
            System.out.println(("People  " + this.allPeople.size()));
            System.out.println(("Pub     " + this.allPublication.size()));
        }
        
        public static void main(String[] args) {
            ConstellationLocationGenerator clg = new ConstellationLocationGenerator();
            clg.fillAllItem();
            clg.showRead();
            clg.writePlace();
        }
    
}