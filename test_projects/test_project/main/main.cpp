// Example program to test CrashExplorer

#include <iostream>
#include <Windows.h>
#include <cassert>
#include <array>

#include "../static_library/quick_sort.cpp"
#include "../static_library/pattern_searcher.hpp"
#include "../dynamic_library/spanning_tree.hpp"
#include "../dynamic_library/math_library.hpp"

class static_class_t
{
public:
  static_class_t()
  {
    int* a = nullptr;
    *a = 42; //crash
  }

};
//static_class_t static_class; //Test manually 

void function_in_main()
{
  while (true)
  {
    std::array<uint8_t, 200> stack_memory_a;
    function_in_main();  //TEST crash in main: stack overflow
  }
}

int main(int argc, char* argv[])
{
  if ((argc > 1) && ((*argv[1] == '-' || (*argv[1] == '/'))))
  {
    int svc_ret = 1;
    if (strcmp("crash_in_main", argv[1] + 1) == 0)
    {
      printf("crash_in_main\n");
      function_in_main();
    }
    else if (strcmp("crash_in_static_lib_a", argv[1] + 1) == 0)
    {
      printf("crash_in_static_lib_a\n");
      quick_sort();//in static lib
    }
    else if (strcmp("crash_in_static_lib_b", argv[1] + 1) == 0)
    {
      printf("crash_in_static_lib_b\n");

      pattern_searcher_t pattern_searcher;
      pattern_searcher.search();//in static lib
    }
    else if (strcmp("crash_in_dynamic_lib_a", argv[1] + 1) == 0)
    {
      printf("crash_in_dynamic_lib_a\n");
      calc_mininum_spaning();
    }
    else if (strcmp("crash_in_dynamic_lib_b", argv[1] + 1) == 0)
    {
      printf("crash_in_dynamic_lib_b\n");
      Cmathlibrary math_library;
      math_library.calc_convex_hull();//in dynamic lib
    }
    else
    {
      printf("Wrong command line option\n");
      assert(false);
    }
  }

  return 0;
}
