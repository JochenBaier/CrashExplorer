#include "quick_sort.hpp"

#include <iostream>


// C++ implementation of QuickSort from: https://www.geeksforgeeks.org/quick-sort/
void swap(int* a, int* b)
{
  int t = *a;
  *a = *b;
  *b = t;
}

int partition(int arr[], int low, int high)
{
  int pivot = arr[high]; // pivot
  int i = (low - 1); // Index of smaller element and indicates the right position of pivot found so far

  for (int j = low; j <= high - 1; j++)
  {
    // If current element is smaller than the pivot
    if (arr[j] < pivot)
    {
      i++; // increment index of smaller element
      
      int* a = nullptr;
      *a = 42; //TEST crash: write access violation
  
      swap(&arr[i], &arr[j]);
    }
  }
  swap(&arr[i + 1], &arr[high]);
  return (i + 1);
}

void quickSort(int arr[], int low, int high)
{
  if (low < high)
  {
    /* pi is partitioning index, arr[p] is now
    at right place */
    int pi = partition(arr, low, high);

    // Separately sort elements before
    // partition and after partition
    quickSort(arr, low, pi - 1);
    quickSort(arr, pi + 1, high);
  }
}

/* Function to print an array */
void printArray(int arr[], int size)
{
  int i;
  for (i = 0; i < size; i++)
    std::cout << arr[i] << " ";
  std::cout << std::endl;
}

void quick_sort() 
{
  int arr[] = { 10, 7, 8, 9, 1, 5 };
  int n = sizeof(arr) / sizeof(arr[0]);
  quickSort(arr, 0, n - 1);
  std::cout << "Sorted array: \n";
  printArray(arr, n);
}
