#include <iostream>
#include <string>
#include <vector>
#include <algorithm>
#include <sstream>
#include <list>
#include <cmath>
#include <iomanip>

using namespace std;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/

struct defib
{
    int num;
    string nom;
    string adresse;
    int numTel;
    long double lo;
    long double la;
};

int main()
{
    string lon;
    cin >> lon; cin.ignore();
    string lat;
    cin >> lat; cin.ignore();
    int n;
    cin >> n; cin.ignore();

    defib DefibTab[n];

    long double dMAX = 99999999;
    int id = 0;

    for (int i = 0; i < n; i++) {
        string defib;
        getline(cin, defib);
        string test = "";

        stringstream ss(defib);
        string data[6];
        int it = 0;

        while(getline(ss, test, ';'))
        {
            data[it] = test;
            it++;
        }

        DefibTab[i].num = stoi(data[0]);
        DefibTab[i].nom = data[1];
        DefibTab[i].adresse = data[2];
        DefibTab[i].numTel = 0;
        string l = data[4];
        string le = data[5];

        size_t pos = l.find(',');
        size_t posle = le.find(',');
        if (pos != string::npos) {
            l.replace(pos, 1, 1, '.');
        }
        if (posle != string::npos) {
            le.replace(posle, 1, 1, '.');
        }

        DefibTab[i].lo = stod(l);
        DefibTab[i].la = stod(le);

        long double go = stod(l);
        long double ga = stod(le);

        go = go * (M_PI/180L);
        ga = ga * (M_PI/180L);

        //cerr << setprecision(15) << go << endl;
        //cerr << setprecision(15) << ga << endl;

        size_t posLA = lat.find(',');
        size_t posLON = lon.find(',');
        if (posLA != string::npos) {
            lat.replace(posLA, 1, 1, '.');
        }
        if (posLON != string::npos) {
            lon.replace(posLON, 1, 1, '.');
        }

        long double latitudePers = stod(lat);
        long double longitudePers = stod(lon);

        latitudePers = latitudePers * (M_PI/180L);
        longitudePers = longitudePers * (M_PI/180L);

        long double x = (go - longitudePers) * cos((ga + latitudePers)/2L);
        long double y = (ga - latitudePers);

        long double d = sqrt(pow(x, 2L) + pow(y, 2L)) * 6371L;

        // long double deltaLat = (ga-latitudePers) * M_PI/180;
        // long double deltaLon = (go-longitudePers) * M_PI/180;

        // long double a = sin(deltaLat/2) * sin(deltaLat/2) + cos(ga) * cos(latitudePers) * sin(deltaLon/2) * sin(deltaLon/2);
        // long double c = 2 * atan2(sqrt(a), sqrt(1-a));

        // long double d = 6371L * c;

        //cerr << setprecision(15) << latitudePers << endl;
        //cerr << setprecision(15) << longitudePers << endl;
        //cerr << " " << endl;
        //cerr << setprecision(15) << DefibTab[i].lo << endl;
        //cerr << setprecision(15) << DefibTab[i].la << endl;
        //cerr << setprecision(15) << d << endl;
        cerr << setprecision(15) << dMAX << endl;
        if(d <= dMAX)
        {
            dMAX = d;
            id = i;
        }
    }

    cout << DefibTab[id].nom << endl;
}