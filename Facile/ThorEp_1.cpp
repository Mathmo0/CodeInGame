#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 * ---
 * Hint: You can use the debug stream to print initialTX and initialTY, if Thor seems not follow your orders.
 **/

int main()
{
    int light_x; // the X position of the light of power
    int light_y; // the Y position of the light of power
    int initial_tx; // Thor's starting X position
    int initial_ty; // Thor's starting Y position
    cin >> light_x >> light_y >> initial_tx >> initial_ty; cin.ignore();

    // game loop
    while (1) {
        int remaining_turns; // The remaining amount of turns Thor can move. Do not remove this line.
        cin >> remaining_turns; cin.ignore();

        // Write an action using cout. DON'T FORGET THE "<< endl"
        // To debug: cerr << "Debug messages..." << endl;

        string dir;

        if(initial_tx == 39)
        {
            if(initial_ty < light_y)
            {
                cout << "S" << endl;     
                initial_ty--;
            }
            else if(initial_ty > light_y)
            {
                cout << "N" << endl;
                initial_ty++;          
            }
        }
        else if(initial_ty == 17)
        {
            if(initial_tx > light_x)
            {
                cout << "W" << endl;
                initial_tx--;
            }
            else if(initial_tx < light_x)
            {
                cout << "E" << endl;
                initial_tx++;
            }
            else if(initial_ty < light_y)
            {
                cerr << "sud" << endl;
                cout << "S" << endl;   
                initial_ty++;         
            }
            else
            {
                cerr << "nord" << endl;
                cout << "N" << endl;   
                initial_ty--;         
            }
        }
        else if(initial_tx != light_x)
        {
            if(initial_ty != light_y)
            {
               if(initial_ty == 17 && initial_tx < light_x)
                {
                    cout << "E" << endl;
                    initial_tx++;
                }
                else if(initial_ty == 17 && initial_tx > light_x)
                {
                    cout << "W" << endl;
                    initial_tx--;
                }
                else if(initial_tx < light_x && initial_ty < light_y)
                {
                    cout << "SE" << endl;
                    initial_tx++;
                    initial_ty++;
                }
                else if(initial_tx < light_x && initial_ty > light_y)
                {
                    cout << "SE" << endl;
                    initial_tx++;
                    initial_ty--;
                }
                else if(initial_tx > light_x && initial_ty > light_y)
                {
                    cerr << "la" << endl;
                    cout << "SW" << endl;
                    initial_tx--;
                    initial_ty--;
                }
                else if(initial_tx > light_x && initial_ty < light_y)
                {
                    cerr << "ici" << endl;
                    cout << "SW" << endl;
                    initial_tx--;
                    initial_ty++;
                }
            }
            else
            {
                if(initial_tx > light_x)
                {
                    cout << "W" << endl;
                    initial_tx--;
                }
                else
                {
                    cout << "E" << endl;
                    initial_tx++;
                }
            }
        }
        else if(initial_ty != light_y)
        {
            cerr << "stp";
            if(initial_ty < light_y)
            {
                cerr << "sud" << endl;
                cout << "S" << endl;   
                initial_ty++;         
            }
            else
            {
                cerr << "nord" << endl;
                cout << "N" << endl;   
                initial_ty--;         
            }
        }
        else
        {
            if(initial_tx < light_x)
            {
                cerr << "bonjour" << endl;
                cout << "E" << endl;
                initial_tx++;
            }
            else
            {
                cerr << "aurevoir" << endl;
                cout << "W" << endl;
                initial_tx--;
            }
        }

        // A single line providing the move to be made: N NE E SE S SW W or NW
        //cout << dir << endl;
    }
}