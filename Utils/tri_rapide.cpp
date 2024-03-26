#include <iostream>

// Fonction pour échanger deux éléments dans le tableau
void swap(int& a, int& b) {
    int temp = a;
    a = b;
    b = temp;
}

// Fonction pour partitionner le tableau et retourner l'index du pivot
int partition(int arr[], int low, int high) {
    int pivot = arr[high]; // On prend le dernier élément comme pivot
    int i = low - 1; // Index du plus petit élément

    for (int j = low; j < high; ++j) {
        // Si l'élément actuel est plus petit ou égal au pivot, on l'échange avec l'élément à l'index i
        if (arr[j] <= pivot) {
            ++i;
            swap(arr[i], arr[j]);
        }
    }

    // Échanger l'élément suivant l'index i avec le pivot
    swap(arr[i + 1], arr[high]);
    return i + 1;
}

// Fonction récursive pour trier le tableau en utilisant le tri rapide
void quickSort(int arr[], int low, int high) {
    if (low < high) {
        int pivotIndex = partition(arr, low, high);

        // Trier les sous-tableaux avant et après le pivot
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

    std::cout << "\nTableau après le tri rapide : ";
    for (int i = 0; i < size; ++i) {
        std::cout << arr[i] << " ";
    }

    return 0;
}
