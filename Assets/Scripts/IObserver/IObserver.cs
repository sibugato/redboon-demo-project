public interface IObserver
{
    public enum Events
    {
        player_sold_item,
        player_bought_item
    }

    public void OnNotify(Events currentEvent);
}
