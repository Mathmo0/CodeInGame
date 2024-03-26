#include <iostream>

// Fonction pour �changer deux �l�ments dans le tableau
void swap(int& a, int& b) {
    int temp = a;
    a = b;
    b = temp;
}

// Fonction pour partitionner le tableau et retourner l'index du pivot
int partition(int arr[], int low, int high) {
    int pivot = arr[high]; // On prend le dernier �l�ment comme pivot
    int i = low - 1; // Index du plus petit �l�ment

    for (int j = low; j < high; ++j) {
        // Si l'�l�ment actuel est plus petit ou �gal au pivot, on l'�change avec l'�l�ment � l'index i
        if (arr[j] <= pivot) {
            ++i;
            swap(arr[i], arr[j]);
        }
    }

    // �changer l'�l�ment suivant l'index i avec le pivot
    swap(arr[i + 1], arr[high]);
    return i + 1;
}

// Fonction r�cursive pour trier le tableau en utilisant le tri rapide
void quickSort(int arr[], int low, int high) {
    if (low < high) {
        int pivotIndex = partition(arr, low, high);

        // Trier les sous-tableaux avant et apr�s le pivot
        quickSort(arr, low, pivotIndex - 1);
        quickSort(arr, pivotIndex + 1, high);
    }
}

int main() {
    int arr[] = { 9, 2, 5, 1, 7, 6, 8, 3, 4 };
    int size = sizeof(arr) / sizeof(arr[0]);

    std::cout << "Tableau avant le tri rapide : ";
    for (int i = 0; i < size; ++i) {
        std::cout << arr[i] << " ";
    }

    quickSort(arr, 0, size - 1);

    std::cout << "\nTableau apr�s le tri rapide : ";
    for (int i = 0; i < size; ++i) {
        std::cout << arr[i] << " ";
    }

    return 0;
}
