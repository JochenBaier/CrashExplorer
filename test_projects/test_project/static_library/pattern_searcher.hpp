#pragma once

#include <string>

class pattern_searcher_t
{
public:
  int search();

private:
  int buildMatchingMachine(std::string arr[], int k);
  int findNextState(int currentState, char nextInput);
  void searchWords(std::string arr[], int k, std::string text);

  const int MAXS = 500;
  const int MAXC = 26;
  int out[500]{};
  int f[500]{};
  int g[500][500]{};
};

