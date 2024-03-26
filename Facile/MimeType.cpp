#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <map>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

int main()
{
    int n; // Number of elements which make up the association table.
    cin >> n; cin.ignore();
    int q; // Number Q of file names to be analyzed.
    cin >> q; cin.ignore();
    map<string, string> type;
    for (int i = 0; i < n; i++) {
        string ext; // file extension
        string mt; // MIME type.
        cin >> ext >> mt; cin.ignore();
        transform(ext.begin(), ext.end(), ext.begin(),
            [](unsigned char c){ return std::tolower(c); });
        type.insert({ext, mt});
    }
    cerr << "   " << endl;
    for (int i = 0; i < q; i++) {
        string fname;
        string res = "";
        getline(cin, fname); // One file name per line.
        int it = fname.find_last_of(".");
        if(it != -1)
            res = fname.substr(it + 1, fname.size());
        
        transform(res.begin(), res.end(), res.begin(),
            [](unsigned char c){ return std::tolower(c); });
        auto t = type.find(res);
        if(t == type.end())
        {
            cout << "UNKNOWN" << endl;
        }
        else
        {
            cout << t->second << endl;
        }
    }

    // Write an answer using cout. DON'T FORGET THE "<< endl"
    // To debug: cerr << "Debug messages..." << endl;


    // For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN.
    //cout << "UNKNOWN" << endl;
}