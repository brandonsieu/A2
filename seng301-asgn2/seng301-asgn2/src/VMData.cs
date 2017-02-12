using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class VMData{

    public int cashReceptacle = 0;
    public List<int> coinKinds = new List<int>();

    public VMData(List<int> coinKind){
        coinKinds.AddRange(coinKind);    
    }

    public void addCash (int cash)
    {
        cashReceptacle += cash;
    }

    public void clearCash()
    {
        cashReceptacle = 0;
    }
    }

