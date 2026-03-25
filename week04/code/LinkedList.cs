using System.Collections;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // I create the new node first so the rest of the method can focus only on wiring it in.
        Node newNode = new(value);

        // If your list is empty, this one node has to become both the head and the tail.
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // Otherwise, I connect the current tail to the new node and then move the tail marker.
        else
        {
            newNode.Prev = _tail; // This lets the new node point back to the old last item.
            _tail.Next = newNode; // This lets the old last item point forward to the new node.
            _tail = newNode; // This finishes the insert by declaring the new node as the tail.
        }
    }


    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }


    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // If head and tail match, your list has either one node or no nodes, so removing tail empties it.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If there are multiple nodes, I only need to detach the current tail and move tail backward.
        else if (_tail is not null)
        {
            _tail.Prev!.Next = null; // The new tail should no longer point to the node we are removing.
            _tail = _tail.Prev; // Shift the tail reference back one node to complete the removal.
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        // Search for the node that matches 'value' by starting at the 
        // head of the list.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the location of 'value' is at the end of the list,
                // then we can call insert_tail to add 'new_value'
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                // For any other location of 'value', need to create a 
                // new node and reconnect the links to insert.
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr; // Connect new node to the node containing 'value'
                    newNode.Next = curr.Next; // Connect new node to the node after 'value'
                    curr.Next!.Prev = newNode; // Connect node after 'value' to the new node
                    curr.Next = newNode; // Connect the node containing 'value' to the new node
                }

                return; // We can exit the function after we insert
            }

            curr = curr.Next; // Go to the next node to search for 'value'
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        // I walk from the head forward because the assignment asks me to remove the first matching value.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                // If the match is the head, I reuse the tested head-removal logic instead of duplicating it.
                if (curr == _head)
                {
                    RemoveHead();
                }
                // If the match is the tail, I reuse the tested tail-removal logic for the same reason.
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                // For a middle node, I reconnect the neighbors around it so the list stays intact.
                else
                {
                    curr.Prev!.Next = curr.Next; // Skip over the current node from the left side.
                    curr.Next!.Prev = curr.Prev; // Skip over the current node from the right side.
                }

                return; // I stop here because only the first matching value should be removed.
            }

            curr = curr.Next; // Keep searching until I find the first match or hit the end.
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        // I scan the whole list because this task wants every matching value updated, not just the first one.
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue; // Only the stored value changes; the node links stay exactly the same.
            }

            curr = curr.Next; // Move forward so every node gets checked.
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        // call the generic version of the method
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        var curr = _head; // Start at the beginning since this is a forward iteration.
        while (curr is not null)
        {
            yield return curr.Data; // Provide (yield) each item to the user
            curr = curr.Next; // Go forward in the linked list
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        // I start at the tail because reverse order means reading the list from back to front.
        var curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data; // Give back the current value before moving one step backward.
            curr = curr.Prev; // Follow the previous link to continue the reverse traversal.
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

public static class IntArrayExtensionMethods {
    public static string AsString(this IEnumerable array) {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
