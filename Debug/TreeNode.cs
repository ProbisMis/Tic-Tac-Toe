using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace minimaxButtons
{
    struct Node2
    {
        public int xcoord;
        public int ycoord;
        public int value;
        

    }

    static class Extensions
    {
        public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
  
    public class TreeNode<T> : IEnumerable<TreeNode<T>>
    {

        public T Data { get; set; } //the node to be added
        public TreeNode<T> Parent { get; set; } //parent 
        public ICollection<TreeNode<T>> Children { get; set; } //list
        
        
        public List<string> myBoard { get; set; }

        public Boolean IsRoot
        {
            get { return Parent == null; }
        }

        public Boolean IsLeaf
        {
            get { return Children.Count == 0; }
        }

        public int Level
        {
            get
            {
                if (this.IsRoot)
                    return 0;
                return Parent.Level + 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            // call the generic version of the method
            return this.GetEnumerator();
        }

        public IEnumerator<TreeNode<T>> GetEnumerator()
        {
            for (int i = 0; i < Children.Count(); i++)
                yield return Children.ElementAt(i);
        }

        public TreeNode(T data)
        {
            this.Data = data;
            this.Children = new LinkedList<TreeNode<T>>();
            //this.myBoard = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            this.myBoard = new List<string>();
            for (int i = 0; i < 9; i++)
            {
                string token = i.ToString();
                myBoard.Add(token);
            }
         
            
           
        }

        public TreeNode<T> AddChild(T child)
        {
            TreeNode<T> childNode = new TreeNode<T>(child) { Parent = this ,  myBoard = Extensions.Clone(this.myBoard)};

           
           
            
            this.Children.Add(childNode);

            
            
           

            return childNode;
        }

        public override string ToString()
        {
            return Data != null ? Data.ToString() : "[data null]";
        }


  
    }
}