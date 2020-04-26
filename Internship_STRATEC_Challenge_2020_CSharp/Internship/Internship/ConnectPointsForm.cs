using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Internship
{
    public partial class ConnectPointsForm : Form
    {
        // - dt is the data source of the data grid view.
        // - pinsList is a list of tuples which contains the coordinates of the original pins that the user enters before pressing on button Route.
        // - addedPinList is a list of tuples which contains the coordinates of all the newly added pins that form the routes and the original pins also.
        // - foundRoute is a boolean that is set to false and every time a new route is discvoered is set to true to indicate that that route is complete.
        // - contRerouting is a contor which will count all times when there is a route which blocks another route and needs to be erased.
        // - minMax is a boolean which indicates a type of choosing the pins that needs to be connected.

        DataTable dt;
        List<Tuple<int, int>> pinsList = new List<Tuple<int, int>>();
        List<Tuple<int, int>> donePinsList = new List<Tuple<int, int>>();
        List<Tuple<int, int>> addedPinsList = new List<Tuple<int, int>>();
        bool foundRoute = false;
        int contRerouting = 0;
        bool minMax = true;
        public ConnectPointsForm()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            this.ReadFromFile();
        }

        //The purpose of this function is to read from file all the rows of the table as strings and send them to initialise the data table.
        private void ReadFromFile()
        {
            try
            {
                string filename = txtFileName.Text;
                List<string[]> listValues = new List<string[]>();
                using (var reader = new StreamReader(filename))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        listValues.Add(values);
                    }
                }
                this.InitialiseDataTable(listValues);

            }
            catch (Exception e)
            {
                MessageBox.Show("The file name is invalid, please enter another one !!!", "Invalid File Name", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // This function will get as parameter a list of strings, each string is a row from the file.
        // The purpose of this function is to initialise the data table with the values from the list, and set the data source for the data grid view.
        private void InitialiseDataTable(List<string[]> listValues)
        {
            dt = new DataTable();
            for (int i = 0; i < listValues[0].Length; i++)
            {
                dt.Columns.Add(i.ToString(), typeof(string));
            }
            for (int i = 0; i < listValues.Count; i++)
            {
                DataRow row1 = dt.NewRow();
                for (int j = 0; j < listValues[0].Length; j++)
                {
                    row1[j.ToString()] = listValues[i][j];
                    if(listValues[i][j]!="0")  pinsList.Add(new Tuple<int, int>(i, j));
                }
                dt.Rows.Add(row1);
            }
            dataGridView1.DataSource = dt;
            dataGridView1.ReadOnly = true;
        }

        // This function will check if the list pinsList is included in addedPinsList, in which case the routing is done and all the pins are connected.
        private bool IsRoutingDone() {
            for (int i = 0; i < pinsList.Count; i++)
                if (addedPinsList.Contains(pinsList[i]) == false) return false;
            return true;
        }

        // This function will remove the single pins that have no match from the pinsList.
        private void CorrectPinsList()
        {
            int ok = 0;
            for (int i = 0; i < pinsList.Count; i++) {
                ok = 0;
                for (int j = 0; j < pinsList.Count && ok==0; j++) {
                    if (i != j && dt.Rows[pinsList[i].Item1][pinsList[i].Item2.ToString()].ToString() == dt.Rows[pinsList[j].Item1][pinsList[j].Item2.ToString()].ToString()) 
                    { 
                        ok = 1;
                    }
                }
                if (ok == 0) pinsList.RemoveAt(i);
            }
        }

        // This function is triggered by clicking the Route button.
        // It's purpose is to connect all the pins with the same value, build all the routes.
        private void btnRoute_Click(object sender, EventArgs e)
        {
            this.CorrectPinsList();
            while (this.IsRoutingDone()==false)
            {
                if (contRerouting == pinsList.Count * pinsList.Count) { 
                    minMax = false;
                    contRerouting = 0;
                }

                // - t will contain on position 0 the start pin and on position1 the end pin.
                // first it will choose the pins that have the same value and are on the same line, vertically/horizontaly, then the other pins. 
                List<Tuple<int, int>> t = this.choosePinsOnTheSameLine();
                if(t[0].Item1==-1 || t[1].Item1 == -1 || t[0].Item2 == -1 || t[1].Item2 == -1) 
                    t=this.choosePinsOnDifferentLines();


                while (foundRoute == false)
                {
                    //ROUTING until the current route can be finished.
                    this.routing(t[0], t[1], t[0].Item1, t[0].Item2, "Start");
                    //If foundRoute is false then eliminate a route and do this route again.
                    if (foundRoute == false)
                    {
                        contRerouting += 1;
                        Tuple<int, int> closestSt = this.closestPin(t[0]);
                        Tuple<int, int> closestEnd = this.closestPin(t[1]);
                        Tuple<int, int> pinToReset;
                        if (this.dist(t[0], closestSt) < this.dist(t[1], closestEnd)) pinToReset = closestSt;
                        else pinToReset = closestEnd;
                        this.resetRoute(pinToReset);
                    }
                }
                // The foundRoute boolean is set to false for the next route.
                // The start and end pins are added to the addedPinsList, that mean that the route containing them is finished.
                foundRoute = false;
                addedPinsList.Add(t[0]);
                if (addedPinsList.Contains(t[1]) == false) addedPinsList.Add(t[1]);
            }
        }

        // This function will take as parameters 2 tuples (start,end) that contain the coordinates of the start pin and end pin of the current route.
        // The purpose of this function is to calculate a rectagular distance between the start and end.
        // The function is used in order to prioritise the chosen routes and to choose the closest routes to some pins.
        private int dist(Tuple<int, int> start, Tuple<int, int> end)
        {
            int x, y;
            if (start.Item1 < end.Item1) x = end.Item1 - start.Item1;
            else x = start.Item1 - end.Item1;
            if (start.Item2 < end.Item2) y = end.Item2 - start.Item2;
            else y = start.Item2 - end.Item2;

            return x + y;
        }

        // This function will take as parameters 2 tuples (start,end) that contain the coordinates of the start pin and end pin of the current route.
        // The purpose of this function is to calculate a number that represent the difference between the sides of a right triangle form by the start and end pins.
        // The function is used in order to prioritise the chosen routes.
        private int distAngle(Tuple<int, int> start, Tuple<int, int> end)
        {
            int x, y;
            if (start.Item1 < end.Item1) x = end.Item1 - start.Item1;
            else x = start.Item1 - end.Item1;
            if (start.Item2 < end.Item2) y = end.Item2 - start.Item2;
            else y = start.Item2 - end.Item2;

            if (x > y) return x - y;
            else return y - x;
        }

        // This function will take as parameters 2 tuples (start,end) that contain the coordinates of the start pin and end pin of the current route.
        // The purpose of this function is to compute a reference direction which the algorithm needs to take from the start to reach end.
        // The direction will be computed each time a new pin is added to the route, such that the algorithm will be constantly "attracted" to reach the end.
        private string direction(Tuple<int, int> start, Tuple<int, int> end)
        {


            if (start.Item1 == end.Item1 && start.Item2 > end.Item2) return "V";
            else if (start.Item1 == end.Item1 && start.Item2 < end.Item2) return "E";
            else if (start.Item1 > end.Item1 && start.Item2 == end.Item2) return "N";
            else if (start.Item1 < end.Item1 && start.Item2 == end.Item2) return "S";
            else if (start.Item1 > end.Item1 && start.Item2 > end.Item2) return "NV";
            else if (start.Item1 > end.Item1 && start.Item2 < end.Item2) return "NE";
            else if (start.Item1 < end.Item1 && start.Item2 < end.Item2) return "SE";
            else return "SV";
        }

        // This function will take as a parameter a pin and will search in the addedPinsList if there is a route with the same values as its.
        // The function will return a boolean value accordingly to the answer.
        private bool IsRouteAdded(Tuple<int, int> pin)
        {
            for (int i = 0; i < addedPinsList.Count; i++)
                if (dt.Rows[pin.Item1][pin.Item2.ToString()].ToString() == dt.Rows[addedPinsList[i].Item1][addedPinsList[i].Item2.ToString()].ToString()) return true;
            return false;
        }

        // This function will choose a start and an end pin that are the closest(or if minMax is false, the furthest) and are on the same line horizontally/vertically.
        // If there are no such pins it will return pins with coordinates that don't exist in the grid.
        private List<Tuple<int, int>> choosePinsOnTheSameLine()
        {
            Tuple<int, int> start = new Tuple<int, int>(-1, -1);
            Tuple<int, int> end = new Tuple<int, int>(-1, -1);
            int minDist;
            if (minMax == true) minDist = dt.Rows.Count + dt.Columns.Count;
            else minDist = -1;
            for (int i = 0; i < pinsList.Count; i++)
            {
                string pinValue = dt.Rows[pinsList[i].Item1][pinsList[i].Item2.ToString()].ToString();
                if (addedPinsList.Contains(pinsList[i]) == false)
                {
                    // This "if" verifies if there is already a route added with the value of the current(i) pin(which is taken as the start pin) and if there is, 
                    // then it will not look in the original pins for an end pin, it will only look in the addedPinsList.

                    if (this.IsRouteAdded(pinsList[i]) == false)
                    {
                        for (int j = 0; j < pinsList.Count; j++)
                            if (i != j && dt.Rows[pinsList[j].Item1][pinsList[j].Item2.ToString()].ToString() == pinValue)
                            {
                                int Dist = this.dist(pinsList[i], pinsList[j]);
                                // This "if" verifies if the pins are on the same line.
                                if (pinsList[i].Item1 == pinsList[j].Item1 || pinsList[i].Item2 == pinsList[j].Item2)
                                {
                                    if (minMax == true)
                                    {
                                        if (Dist < minDist)
                                        {
                                            start = pinsList[i];
                                            end = pinsList[j];
                                            minDist = Dist;
                                        }
                                    }
                                    else
                                    {
                                        if (Dist > minDist)
                                        {
                                            start = pinsList[i];
                                            end = pinsList[j];
                                            minDist = Dist;
                                        }
                                    }
                                }
                            }
                    }
                    for (int j = 0; j < addedPinsList.Count; j++)
                        if (dt.Rows[addedPinsList[j].Item1][addedPinsList[j].Item2.ToString()].ToString() == pinValue)
                        {
                            int Dist = this.dist(pinsList[i], addedPinsList[j]);
                            // This "if" verifies if the pins are on the same line.
                            if (pinsList[i].Item1 == addedPinsList[j].Item1 || pinsList[i].Item2 == addedPinsList[j].Item2)
                            {
                                if (minMax == true)
                                {
                                    if (Dist < minDist)
                                    {
                                        start = pinsList[i];
                                        end = addedPinsList[j];
                                        minDist = Dist;
                                    }
                                }
                                else
                                {
                                    if (Dist > minDist)
                                    {
                                        start = pinsList[i];
                                        end = addedPinsList[j];
                                        minDist = Dist;
                                    }
                                }
                            }
                        }
                }
            }
            return new List<Tuple<int, int>> { start, end };
        }

        // This function will choose a start and an end pin that are on different lines(diagonally), 
        // taking into account the distAngle of them to be the biggest/lowest, depending of the minMax boolean.
        // If there are no such pins it will return pins with coordinates that don't exist in the grid.
        private List<Tuple<int, int>> choosePinsOnDifferentLines()
        {
            Tuple<int, int> start = new Tuple<int, int>(-1, -1);
            Tuple<int, int> end = new Tuple<int, int>(-1, -1);
            int minDist;
            if (minMax == true) minDist = -1;
            else minDist = dt.Rows.Count + dt.Columns.Count;
            for (int i = 0; i < pinsList.Count; i++)
            {
                string pinValue = dt.Rows[pinsList[i].Item1][pinsList[i].Item2.ToString()].ToString();
                if (addedPinsList.Contains(pinsList[i]) == false)
                {

                    // This "if" verifies if there is already a route added with the value of the current(i) pin(which is taken as the start pin) and if there is, 
                    // then it will not look in the original pins for an end pin, it will only look in the addedPinsList.
                    if (this.IsRouteAdded(pinsList[i]) == false)
                    {
                        for (int j = 0; j < pinsList.Count; j++)
                            if (i != j && dt.Rows[pinsList[j].Item1][pinsList[j].Item2.ToString()].ToString() == pinValue)
                            {
                                int Dist = this.distAngle(pinsList[i], pinsList[j]);
                                if (pinsList[i].Item1 != pinsList[j].Item1 && pinsList[i].Item2 != pinsList[j].Item2)
                                {
                                    if (minMax == true)
                                    {
                                        if (Dist > minDist)
                                        {
                                            start = pinsList[i];
                                            end = pinsList[j];
                                            minDist = Dist;
                                        }
                                    }
                                    else
                                    {
                                        if (Dist < minDist)
                                        {
                                            start = pinsList[i];
                                            end = pinsList[j];
                                            minDist = Dist;
                                        }
                                    }
                                }
                            }
                    }
                    for (int j = 0; j < addedPinsList.Count; j++)
                        if (dt.Rows[addedPinsList[j].Item1][addedPinsList[j].Item2.ToString()].ToString() == pinValue)
                        {
                            int Dist = this.distAngle(pinsList[i], addedPinsList[j]);
                            if (pinsList[i].Item1 != addedPinsList[j].Item1 && pinsList[i].Item2 != addedPinsList[j].Item2)
                            {
                                if (minMax == true)
                                {
                                    if (Dist > minDist)
                                    {
                                        start = pinsList[i];
                                        end = addedPinsList[j];
                                        minDist = Dist;
                                    }
                                }
                                else
                                {
                                    if (Dist < minDist)
                                    {
                                        start = pinsList[i];
                                        end = addedPinsList[j];
                                        minDist = Dist;
                                    }
                                }
                            }
                        }
                }
            }
            return new List<Tuple<int, int>> { start, end };
        }

        // This function will make all the validations for the pin with coordinates(i,j), 
        // taking into account the coordinates of the start and end pin and also the direction that the route will take.
        // If the pin is valid then the value of it will be changed into the value of the route, the pin will be added to addedPinsList
        // and the function routing will be called on it, when the route will found it's end or when it will block itself 
        // it will come back to execute the remaining code, remove the pin from the addedPinsList and reset the value from the data table if it will be necessary.
        private void OnePinRouting(Tuple<int, int> start, Tuple<int, int> end, int i, int j, string newDirection)
        {
            if (this.AreIndexesValid(i, j, end) == true && foundRoute == false && this.IsPinValid(i, j, dt.Rows[start.Item1][start.Item2.ToString()].ToString()) == true)
            {
                dt.Rows[i][j.ToString()] = dt.Rows[start.Item1][start.Item2.ToString()].ToString();
                Tuple<int, int> pinToAdd = new Tuple<int, int>(i, j);
                addedPinsList.Add(pinToAdd);
                routing(start, end, i, j, newDirection);
                if (foundRoute == false)
                {
                    dt.Rows[i][j.ToString()] = "0";
                    addedPinsList.Remove(pinToAdd);
                }
            }
        }

        // This function will move from a pin to another, from the start pin to the end one, update the direction constantly 
        // and trying to build the corresponding route.
        private void routing(Tuple<int, int> start, Tuple<int, int> end, int i, int j, string LastDirection)
        {
            string nextDirection = this.direction(new Tuple<int, int>(i, j), end);

            if (end.Item1 == i && end.Item2 == j)
            {
                foundRoute = true;
            }
            else
            {
                if (nextDirection == "N")
                {
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                }
                if (nextDirection == "S")
                {
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                }
                if (nextDirection == "E")
                {
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                }
                if (nextDirection == "V")
                {
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                }
                if (nextDirection == "NV")
                {
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                }
                if (nextDirection == "NE")
                {
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                }
                if (nextDirection == "SV")
                {
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                }
                if (nextDirection == "SE")
                {
                    if (LastDirection != "Up") OnePinRouting(start, end, i + 1, j, "Down");
                    if (LastDirection != "Left") OnePinRouting(start, end, i, j + 1, "Right");
                    if (LastDirection != "Down") OnePinRouting(start, end, i - 1, j, "Up");
                    if (LastDirection != "Right") OnePinRouting(start, end, i, j - 1, "Left");
                }
            }
        }

        // This function will find the closest pin from a given pin.
        // It is used in finding the closest route to be erased.
        private Tuple<int, int> closestPin(Tuple<int, int> pin)
        {
            Tuple<int, int> closest = new Tuple<int, int>(-1, -1);
            int minDist = dt.Rows.Count + dt.Columns.Count;

            for (int i = 0; i < addedPinsList.Count; i++)
            {
                if (addedPinsList[i] != pin && dt.Rows[pin.Item1][pin.Item2.ToString()].ToString() != dt.Rows[addedPinsList[i].Item1][addedPinsList[i].Item2.ToString()].ToString())
                {
                    int Dist = this.dist(pin, addedPinsList[i]);
                    if (Dist < minDist)
                    {
                        minDist = Dist;
                        closest = addedPinsList[i];
                    }
                }
            }
            return closest;
        }

        // This function will take as parameter a tuple containg the coordinates of a pin.
        // It's purpose is to find the route with the same value as pin's and erase it to give space for other routes to be made.
        private void resetRoute(Tuple<int, int> pin)
        {
            string pinValue = dt.Rows[pin.Item1][pin.Item2.ToString()].ToString();

            for (int i = 0; i < addedPinsList.Count(); i++)
            {
                if (dt.Rows[addedPinsList[i].Item1][addedPinsList[i].Item2.ToString()].ToString() == pinValue)
                {
                    if (pinsList.Contains(addedPinsList[i]) == false) dt.Rows[addedPinsList[i].Item1][addedPinsList[i].Item2.ToString()] = "0";
                    addedPinsList.RemoveAt(i);
                }
            }
        }

        // This function will verify the coordinates(i,j) to be in grid.
        // It will return true if they are the coordinates of the end pin.
        // It will return false if the pin is already added to the addPinsList.
        private bool AreIndexesValid(int i, int j, Tuple<int, int> end)
        {
            if (end.Item1 == i && end.Item2 == j) return true;
            if (addedPinsList.Contains(new Tuple<int, int>(i, j)) == true) return false;
            if (i < 0 || i >= dt.Rows.Count) return false;
            if (j < 0 || j >= dt.Columns.Count) return false;
            return true;
        }

        // This function will verrify if the pin with coordinates (i,j) is valid according to the routes requirements.
        private bool IsPinValid(int i, int j, string startPinValue)
        {
            string pinValue = dt.Rows[i][j.ToString()].ToString();
            if (j - 1 >= 0 && dt.Rows[i][(j - 1).ToString()].ToString() != "0" && dt.Rows[i][(j - 1).ToString()].ToString() != startPinValue) return false;
            if (j + 1 < dt.Columns.Count && dt.Rows[i][(j + 1).ToString()].ToString() != "0" && dt.Rows[i][(j + 1).ToString()].ToString() != startPinValue) return false;
            if (i - 1 >= 0 && j - 1 >= 0 && dt.Rows[i - 1][(j - 1).ToString()].ToString() != "0" && dt.Rows[i - 1][(j - 1).ToString()].ToString() != startPinValue) return false;
            if (i - 1 >= 0 && j + 1 < dt.Columns.Count && dt.Rows[i - 1][(j + 1).ToString()].ToString() != "0" && dt.Rows[i - 1][(j + 1).ToString()].ToString() != startPinValue) return false;
            if (i - 1 >= 0 && dt.Rows[i - 1][(j).ToString()].ToString() != "0" && dt.Rows[i - 1][(j).ToString()].ToString() != startPinValue) return false;
            if (i + 1 < dt.Rows.Count && j - 1 >= 0 && dt.Rows[i + 1][(j - 1).ToString()].ToString() != "0" && dt.Rows[i + 1][(j - 1).ToString()].ToString() != startPinValue) return false;
            if (i + 1 < dt.Rows.Count && j + 1 < dt.Columns.Count && dt.Rows[i + 1][(j + 1).ToString()].ToString() != "0" && dt.Rows[i + 1][(j + 1).ToString()].ToString() != startPinValue) return false;
            if (i + 1 < dt.Rows.Count && dt.Rows[i + 1][(j).ToString()].ToString() != "0" && dt.Rows[i + 1][(j).ToString()].ToString() != startPinValue) return false;
            return true;
        }

    }
}
