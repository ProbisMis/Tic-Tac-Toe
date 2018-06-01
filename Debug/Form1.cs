using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace minimaxButtons
{
    
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int depth;
        double alfa;
        double beta;
        bool turn = true;
        int drawcondition = 0;
       // int[,] myBoard = new int[3,3];
        TreeNode<Node2> myNewtree;
        List<int[]> myBoard;
        
       
        //reset the main board
        private void resetmyBoard(int[,] board) {
            for (int i = 0; i < 3; i++ )
            {
                for (int j = 0; j < 3; j++) {
                    board[i,j] = 0;
                    
                }

            }

        
        
        }

        private void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
            {
                String name = b.Name;
                nameToBoard(name);
                
                b.Text = "X";

            }
            else {
                String name = b.Name;
                nameToBoard(name);
                minmax(myNewtree, depth, alfa, beta, turn);
                //generateChildStates();
                b.Text = "0";

            }
               
            turn = !turn;
            b.Enabled = false;
            drawcondition++;

            checkWinner();
        }

        //version 2 (TreeNode.cs) perfectly works
        private void generateChildStates(TreeNode<Node2> cur, int depth, int statecount)
        {
          

            //initially empty tree sended and child list is created in constructor
            // now childs needs to be added the current node
            //     1.child needs to be created with board and new list of child node's 
            //     2.child boards needs to be updated

            if (depth == 0)
            {
                //end condition add child here 
                return;
            }
            
            
            
            int state = 0;
            

            //loop for states 9-8-7-...
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cur.myBoard[i][j] == 0)
                    {

                        //condition 2.
                        
                        Node2 newNode = new Node2();
                        newNode.xcoord = i;
                        newNode.ycoord = j;
                        cur.AddChild(newNode);
                        cur.Children.ElementAt(state).myBoard[i][j] = 1;

                        generateChildStates(cur.Children.ElementAt(state), depth - 1, statecount - 1);



                        state++;
                    }
                }

            }
        }
         
        //return list of empty indexes in current node
        public List<int> getEmptyIndexes(TreeNode<Node2> tree)
        {


            List<int> emptyIndexes = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tree.myBoard[i][j] == 0)
                    {
                        emptyIndexes.Add(IndexEquilavent(i, j));
                    }

                }

            }
            return emptyIndexes;
        }
       
        //helper function for getEmptyIndexes
        public int IndexEquilavent(int x, int y) {
            if (x == 0 && y == 0)
                return 0;
            else if (x == 0 && y == 1)
                return 1;
            else if (x == 0 && y == 2)
                return 2;
            else if (x == 1 && y == 0)
                return 3;
            else if (x == 1 && y == 1)
                return 4;
            else if (x == 1 && y == 2)
                return 5;
            else if (x == 2 && y == 0)
                return 6;
            else if (x == 2 && y == 1)
                return 7;
            else
                return 8;
        
            
        }
        
     

        //tick the board from checking the name(index) of button
        public int nameToBoard(String name)
        {
            if (name == "button0"){
                myBoard[0][0] = 1;
                return 0;
            }

            else if (name == "button1")
            {
                myBoard[0][1] = 1;
                return 1;
            }
            else if (name == "button2")
            {
                myBoard[0][2] = 1;
                return 2;
            }
            else if (name == "button3")
            {
                myBoard[1][0] = 1;
                return 3;
            }
            else if (name == "button4")
            {
               myBoard[1][1] = 1;
               return 4;           
            }
            else if (name == "button5") {
                myBoard[1][2] = 1;
                return 5;
            }
                
            else if (name == "button6"){
                myBoard[2][0] = 1;
                return 6;
            }

            else if (name == "button7")
            {
                myBoard[2][1] = 1;
                return 7;
            }
            else if (name == "button8")
            {
                myBoard[2][2] = 1;
                return 8;
            }

            return 9;
        
        
        }

        //at the of game this will come 
        private int checkWinner() {
            bool thewinner = false;

            //horizontal check
            if (button0.Text == button1.Text && button1.Text == button2.Text && (!button0.Enabled))
            {
                thewinner = true;
            }
            else if (button3.Text == button4.Text && button4.Text == button5.Text && (!button3.Enabled))
            {
                thewinner = true;
            }
            else if (button6.Text == button7.Text && button7.Text == button8.Text && (!button6.Enabled))
            {
                thewinner = true;
            }

            //vertical check
            if (button0.Text == button3.Text && button3.Text == button6.Text && (!button0.Enabled))
            {
                thewinner = true;
            }
            else if (button1.Text == button4.Text && button4.Text == button7.Text && (!button1.Enabled))
            {
                thewinner = true;
            }
            else if (button2.Text == button5.Text && button5.Text == button8.Text && (!button2.Enabled))
            {
                thewinner = true;
            }

            //diagonal check
            if (button0.Text == button4.Text && button4.Text == button8.Text && (!button0.Enabled))
            {
                thewinner = true;
            }
            else if (button2.Text == button4.Text && button4.Text == button6.Text && (!button2.Enabled))
            {
                thewinner = true;
            }
           

            if (thewinner)
            {
                disableButtons();

                String winner = "";
                if (turn)
                {
                    winner = "O";
                    label6.Text = (Int32.Parse(label6.Text)+ 1).ToString();
                }
                else
                {
                    winner = "X";
                    label4.Text = (Int32.Parse(label4.Text) + 1).ToString();
                }
                MessageBox.Show("The winner is:" + winner, "The end");

                resetBoard();
                if (winner == "0")
                    return -10;
                else if (winner == "X")
                    return 10;

            }
            else {
                if (drawcondition == 9) {
                    label5.Text = (Int32.Parse(label5.Text) + 1).ToString();
                    MessageBox.Show("DRAW");
                    resetBoard();
                    return 0;
                }
                return heuristic;
            
            }
                
                
                
        
        }

        //helper function to block continuing the game
        private void disableButtons() {

           
                foreach (Control c in Controls)
                {
                     try
                 {
                         Button b = (Button)c;
                         b.Enabled = false;
                 }
                     catch { }
                }
           
           
            
        
        }

        //stylish
        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (b.Enabled) {
                
                if (turn)
                    b.Text = "X";
                else
                    b.Text = "O";
            
            }

        }
        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            if (b.Enabled)
            {

                b.Text = "";

            }
        }

        //heuristic of button.Text
        private int heuristic() {
            int rcount = 0, ccount = 0,hcount=0;
            int rocount = 0, cocount = 0, hocount = 0;

           //column check for X 
            if (button0.Text == "O" || button3.Text == "O" || button6.Text == "O") { 
            }
            else
                ccount++;

            if (button1.Text == "O" || button4.Text == "O" || button7.Text == "O") { 
            }
            else
                ccount++;

            if (button2.Text == "O" || button5.Text == "O" || button8.Text == "O"){
            }
            else
                ccount++;

            //row check for X
            if (button0.Text == "O" || button1.Text == "O" || button2.Text == "O")
            {
            }
            else
                rcount++;

            if (button3.Text == "O" || button4.Text == "O" || button5.Text == "O")
            {
            }
            else
                rcount++;

            if (button6.Text == "O" || button7.Text == "O" || button8.Text == "O")
            {
            }
            else
                rcount++;

            //horizontal check for X
            if (button0.Text == "O" || button4.Text == "O" || button8.Text == "O")
            {
            }
            else
                hcount++;

            if (button6.Text == "O" || button4.Text == "O" || button2.Text == "O")
            {
            }
            else
                hcount++;


            //row check for O
            if (button0.Text == "X" || button1.Text == "X" || button2.Text == "X")
            {
            }
            else
                rocount++;

            if (button3.Text == "X" || button4.Text == "X" || button5.Text == "X")
            {
            }
            else
                rocount++;

            if (button6.Text == "X" || button7.Text == "X" || button8.Text == "X")
            {
            }
            else
                rocount++;

            //column check for O
            if (button0.Text == "X" || button3.Text == "X" || button6.Text == "X")
            {
            }
            else
                cocount++;

            if (button1.Text == "X" || button4.Text == "X" || button7.Text == "X")
            {
            }
            else
                cocount++;

            if (button2.Text == "X" || button5.Text == "X" || button8.Text == "X")
            {
            }
            else
                cocount++;

            //horizontal check for X
            if (button0.Text == "X" || button4.Text == "X" || button8.Text == "X")
            {
            }
            else
                hocount++;

            if (button6.Text == "X" || button4.Text == "X" || button2.Text == "X")
            {
            }
            else
                hocount++;



            return (rcount + hcount + ccount) - (rocount + hocount + cocount);
        
        } 

        private int heruisticBoard(

        private void resetBoard() 
        
        {
            turn = true;
            drawcondition = 0;
            
            foreach (Control c in Controls)
            {
                try
                {
                    Button b = (Button)c;
                    b.Text = "";
                    b.Enabled = true;
                }
                catch { }
            }
        
        }

        private double minmax(TreeNode<Node2> currentNode,int depth,double  alfa, double beta, bool turn) {
            
            double v;
            List<double> scores = new List<double>();
            List<double> moves = new List<double>();
            List<int> emptyIndexes;

            emptyIndexes = getEmptyIndexes(currentNode);
            

            //i need avaliable spots.
            // i need moves array of move object with index and score
            
            if(depth == 0 || currentNode.Children.Count() == 0 || checkWinner() != null) //if winner return +10 or -10 if draw return 0
                return heuristic();

            if (turn)
            {
                v = double.NegativeInfinity ;
                for (int i = 0; i < currentNode.Children.Count(); i++)
                {
                    v= Math.Max(v,minmax(currentNode.Children.ElementAt(i),depth-1,alfa,beta,!turn));
                   
                    scores.Add(v);
                                     
                    alfa= Math.Max(alfa,v);
                    if(beta <= alfa)
                        break;

                   
                }
                 return v;
            }
            else { 
                v = double.PositiveInfinity;
                for (int i = 0; i < currentNode.Children.Count(); i++)
                {
                    v = Math.Min(v, minmax(currentNode.Children.ElementAt(i), depth - 1, alfa, beta, !turn));
                    
                    
                    beta=Math.Min(beta,v);
                    if(beta<=alfa)
                        break;


                   
                }
            
             return v;
            }

            
        
        
        
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Node2 newNode = new Node2();
            myNewtree = new TreeNode<Node2>(newNode);
            myBoard= new List<int[]>();
            comboBox1.SelectedIndex = 4;
            depth = Int32.Parse(comboBox1.Text);

            generateChildStates(myNewtree, depth, 9);
        }
    }
}
