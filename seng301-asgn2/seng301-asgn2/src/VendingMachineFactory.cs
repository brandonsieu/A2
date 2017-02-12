using System;
using System.Collections.Generic;
using Frontend2;
using Frontend2.Hardware;

public class VendingMachineFactory : IVendingMachineFactory {

    List<VendingMachine> theVendingMachineFactory = new List<VendingMachine>();
    List<VMData> theVMLogics = new List<VMData>();

    public int CreateVendingMachine(List<int> coinKinds, int selectionButtonCount, int coinRackCapacity, int popRackCapcity, int receptacleCapacity) {
        theVendingMachineFactory.Add(new VendingMachine(coinKinds.ToArray(), selectionButtonCount, coinRackCapacity, popRackCapcity, receptacleCapacity));
        theVMLogics.Add(new VMData(coinKinds));
        // TODO: Implement
        return theVendingMachineFactory.Count-1;
    }

    public void ConfigureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (config).");
        theVendingMachineFactory[vmIndex].Configure(popNames, popCosts);
        // TODO: Implement
    }

    public void LoadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (LC).");
        VendingMachine theMachine = theVendingMachineFactory[vmIndex];
        if (coinKindIndex < 0 || coinKindIndex >= theMachine.CoinRacks.Length) throw new Exception("Null coinkindIndex (LC).");
        theMachine.CoinRacks[coinKindIndex].LoadCoins(coins);
    // TODO: Implement
}

    public void LoadPops(int vmIndex, int popKindIndex, List<PopCan> pops) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (LP).");
        VendingMachine theMachine = theVendingMachineFactory[vmIndex];
        if (popKindIndex < 0 || popKindIndex >= theMachine.PopCanRacks.Length) throw new Exception("Null popkindIndex (LP).");
        theMachine.PopCanRacks[popKindIndex].LoadPops(pops);
        // TODO: Implement
    }

    public void InsertCoin(int vmIndex, Coin coin) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (IC).");
        VendingMachine theMachine = theVendingMachineFactory[vmIndex];
        theMachine.CoinSlot.AddCoin(coin);
        if (theVMLogics[vmIndex].coinKinds.Contains(coin.Value)) theVMLogics[vmIndex].addCash(coin.Value);
        // TODO: Implement
    }

    public void PressButton(int vmIndex, int value) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (PB).");
        VendingMachine theMachine = theVendingMachineFactory[vmIndex];
        if (value < 0 || theMachine.SelectionButtons.Length <= value) throw new Exception("Null value (PB).");
        if (theMachine.PopCanCosts[value] <= theVMLogics[vmIndex].cashReceptacle){
            theMachine.PopCanRacks[value].DispensePopCan();
            theMachine.CoinReceptacle.StoreCoins();
            int change = theVMLogics[vmIndex].cashReceptacle - theMachine.PopCanCosts[value];
            List<int> sortedCoins = new List<int>();
            sortedCoins.AddRange(theVMLogics[vmIndex].coinKinds);
            sortedCoins.Sort();
            while (change > 0)
            {
                int coinValue = sortedCoins[sortedCoins.Count - 1];
                int modChange = change / coinValue;
                CoinRack theCoinRack = theMachine.GetCoinRackForCoinKind(coinValue);
                int amountRemoved = Math.Min(theCoinRack.Count, modChange);
                change -= amountRemoved * coinValue;
                for (int i = 0; i < amountRemoved; i++) theCoinRack.ReleaseCoin();
                sortedCoins.RemoveAt(sortedCoins.Count - 1);
                if (sortedCoins.Count == 0) break;
            }
            theVMLogics[vmIndex].cashReceptacle = 0;
        }
        // TODO: Implement
    }

    public List<IDeliverable> ExtractFromDeliveryChute(int vmIndex) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (EFD).");
        VendingMachine theMachine = theVendingMachineFactory[vmIndex];
        List<IDeliverable> deliveryContents = new List<IDeliverable>();
        foreach(IDeliverable item in theMachine.DeliveryChute.RemoveItems()) deliveryContents.Add(item);
        // TODO: Implement
        return deliveryContents;
    }

    public VendingMachineStoredContents UnloadVendingMachine(int vmIndex) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (UVM).");
        VendingMachine theMachine = theVendingMachineFactory[vmIndex];
        VendingMachineStoredContents theMachineContents = new VendingMachineStoredContents();
        for (int i = 0; i < theMachine.CoinRacks.Length; i++) theMachineContents.CoinsInCoinRacks.Add(theMachine.CoinRacks[i].Unload());
        for (int i = 0; i < theMachine.PopCanRacks.Length; i++) theMachineContents.PopCansInPopCanRacks.Add(theMachine.PopCanRacks[i].Unload());
        foreach(Coin profit in theMachine.StorageBin.Unload()) theMachineContents.PaymentCoinsInStorageBin.Add(profit);
        // TODO: Implement
        return theMachineContents;
    }
}
