// The following ifdef block is the standard way of creating macros which make exporting
// from a DLL simpler. All files within this DLL are compiled with the MATHLIBRARY_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see
// MATHLIBRARY_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef MATHLIBRARY_EXPORTS
#define MATHLIBRARY_API __declspec(dllexport)
#else
#define MATHLIBRARY_API __declspec(dllimport)
#endif

#include <stack>

struct Point
{
  int x, y;
};

// This class is exported from the dll
class MATHLIBRARY_API Cmathlibrary
{
public:
  Cmathlibrary(void)
  {
  }
  int calc_convex_hull();

private:
  Point nextToTop(std::stack<Point>& S);
  void swap(Point& p1, Point& p2);
  void convexHull(Point points[], int n);

};


