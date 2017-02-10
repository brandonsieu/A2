using System;
using System.Collections.Generic;
using Frontend2;
using Frontend2.Hardware;

public class VendingMachineFactory : IVendingMachineFactory {

    List<VendingMachine> theVendingMachineFactory = new List<VendingMachine>();

    public int CreateVendingMachine(List<int> coinKinds, int selectionButtonCount, int coinRackCapacity, int popRackCapcity, int receptacleCapacity) {
        theVendingMachineFactory.Add(new VendingMachine(coinKinds.ToArray(), selectionButtonCount, coinRackCapacity, popRackCapcity, receptacleCapacity));
        // TODO: Implement
        return 0;
    }

    public void ConfigureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (config).");
        theVendingMachineFactory[vmIndex].Configure(popNames, popCosts);
        // TODO: Implement
    }

    public void LoadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {
        if (vmIndex < 0 || vmIndex >= theVendingMachineFactory.Count) throw new Exception("Null vmIndex (LC).");
        if (coinKindIndex < 0 || coinKindIndex >= theVendingMachineFactory[vmIndex].CoinRacks.Length) throw new Exception("Null coinkindIndex (LC).");
        int[] coinstoLoad = new int[theVendingMachineFactory[vmIndex].CoinRacks.Length];
       // cointstoLoad[coinKindIndex].addRange(coins);
        //theVendingMachineFactory[vmIndex].LoadCoins(coins);
    // TODO: Implement
}

    public void LoadPops(int vmIndex, int popKindIndex, List<PopCan> pops) {
        // TODO: Implement
    }

    public void InsertCoin(int vmIndex, Coin coin) {
        // TODO: Implement
    }

    public void PressButton(int vmIndex, int value) {
        // TODO: Implement
    }

    public List<IDeliverable> ExtractFromDeliveryChute(int vmIndex) {
        // TODO: Implement
        return new List<IDeliverable>();
    }

    public VendingMachineStoredContents UnloadVendingMachine(int vmIndex) {
        // TODO: Implement
        return new VendingMachineStoredContents();
    }
}