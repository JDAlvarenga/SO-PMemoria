using PMemoria.Util;

namespace PMemoria.Services;

public class OptimalMemoryService
{
    public int Capacity { get; private set; }

    public List<int> Requests { get; private set; } = [];
    
    // Holds the frame each page is stored in
    public Dictionary<int, int> PagesInFrame { get; } = [];

    // Maps each page to a list containing the next times it will be requested. 
    public Dictionary<int, List<int>> NextRequests { get; }= new();
    
    // The reverse of the above. Maps the position of the next request to the page, ONLY for the pages in a frame. 
    // The priority is the position of the next request (max heap)
    public PriorityQueue<int, int> RequestQueue { get; } = new(new DescComparer<int>());
    

    public (int page, int frame)? Next()
    {
        if (Requests.Count == 0) return null;
        
        var page = Requests[0];
        Requests.RemoveAt(0);

        var frame = RequestPage(page);
        return (page, frame);
    }

    public void LoadRequests(List<int> requests, int capacity = -1)
    {
        if (capacity > 0) Capacity = capacity;
        Requests = requests;
        
        NextRequests.Clear();
        PagesInFrame.Clear();
        RequestQueue.Clear();
        var idx = 0;
        foreach (var request in Requests)
        {
            if (!NextRequests.TryGetValue(request, out var nextList))
            {
                nextList = [];
                NextRequests.Add(request,nextList);
            }
            nextList.Add(idx++);
        }
    }

    
    /// <summary>
    ///  Processes the page request
    /// </summary>
    /// <returns>The frame in which the requested page can be accessed.</returns>
    private int RequestPage(int page)
    {
        // Already in memory
        if (PagesInFrame.TryGetValue(page, out var frame))
        {
            HandleRequest(page);
            return frame;
        }
        
        // Not in memory but within capacity
        if (PagesInFrame.Count < Capacity)
        {
            frame = PagesInFrame.Count;
            AddPage(page, frame);
            return frame;
        }
        
        // At capacity
        
        var furthestReqPage = RequestQueue.Peek();
        frame = PagesInFrame[furthestReqPage];
        RemovePage(furthestReqPage);
        AddPage(page, frame);
        
        return frame;
    }

    private void RemovePage(int page)
    {
        PagesInFrame.Remove(page);
        RequestQueue.Remove(page, out var _, out var _);
    }

    private void AddPage(int page, int frame)
    {
        HandleRequest(page);
        PagesInFrame.Add(page, frame);
    }
    
    /// <summary>
    /// Updates de internal state for a page request
    /// </summary>
    /// <param name="page">The requested page.</param>
    private void HandleRequest(int page)
    {
        if (NextRequests[page].Count > 0)
        {
            NextRequests[page].RemoveAt(0);
        }
        var nextRequest = NextRequests[page].Count == 0 ? int.MaxValue : NextRequests[page][0];
        
        // replace the value in queue with next time page will be requested.
        RequestQueue.Remove(page, out var _, out var _);
        RequestQueue.Enqueue(page, nextRequest);
    }
}








