public class CycleIndex {
    int currentIndex = 0;
    int size = 1;

    public CycleIndex(int size, int current = 0) {
        this.size = size;
        if(size <= 0) {
            this.size = 1;
        }
        currentIndex = current;
    }

    public int Next {
        get {
            currentIndex++;
            if(currentIndex >= size) currentIndex = 0;
            return currentIndex;
        }
    }

    public int Prev {
        get {
            currentIndex--;
            if(currentIndex < 0) currentIndex = size - 1;
            return currentIndex;
        }
    }

    public int this[int index] {
        get {
            int tmp = 0;
            if(index >= 0) {
                tmp = index % size;
            }
            if(index < 0) {
               var mod = (-1*index-1)% size;
                tmp = size - (mod+1);
            }
            return tmp;
        }
    }

    public void Set(int index) {
        currentIndex = this[index];
    }

    static public string SelfTest() {
        string outPutString = "***";
        CycleIndex index = new CycleIndex(10);
        for(int i = -20; i < 20; i++) {
            outPutString += index[i] + ",";
        }
        outPutString += "\n***";
        var tmp = index[-20];
        for(int i = 0; i < 40; i++) {
            outPutString += index.Next + ",";
        }
        outPutString += "\n***";
        for(int i = 0; i < 40; i++) {
            outPutString += index.Prev + ",";
        }
        return outPutString;
    }
}
