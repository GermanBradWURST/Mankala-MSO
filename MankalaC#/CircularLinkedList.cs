namespace Mankala
{
    //Making nodes that are present in the linkedlist
    //they have data and a reference to the next node
    class Node<Pit>
    {
        public Pit Data {get; set;}
        public Node<Pit> Next {get; set;}
    }

    //creating own circular linkedlist class for creating a board
    //normal linkedlist doesn't allow for the tail to point back to the head, this does
    class CircularLinkedList<Pit> 
    {
        private Node<Pit> head;
        private Node<Pit> tail;
        public int Count {get; private set;}

        public void AddLast(Pit Data)
        {
            Node<Pit> newNode = new Node<Pit> {Next = null, Data = Data};

            if(head == null)
            {
                head = newNode;
                tail = newNode;
                tail.Next = head;
            }
            else
            {
                tail.Next = newNode;
                tail = newNode;
                tail.Next = head;
            }
            Count++;
        }


        //this function is yet to be created, it will alow us to loop through our circular
        //linkedlist with foreach
        //public IEnumerator<Pit> GetEnumerator()
        //{
        //}
    }
}