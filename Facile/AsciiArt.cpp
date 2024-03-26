#include <iostream>
#include <string>
#include <vector>
#include <algorithm>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

int main()
{
    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ?";
    
    int l;
    cin >> l; cin.ignore();
    int h;
    cin >> h; cin.ignore();
    string t;
    getline(cin, t);
    transform(t.begin(), t.end(), t.begin(), ::toupper);
    for (int i = 0; i < h; i++) {
        string res = "";
        string row;
        getline(cin, row);
        for(char c : t)
        {
            int it = alphabet.find(c);
            if(it != -1)
            {
                res += row.substr(l * it, l);
            }
            else
                res += row.substr(l * 26, l);
        }
        cout << res << endl;
    }

    // Write an answer using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;

    //cout << res << endl;
}