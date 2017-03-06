# 8INF870-02
```csharp
ProchainChampignon(int x, int y, int score) {
  score++;
  int currentBestScore=score;
  Champignon currentBestNext;
  for(int i=x;i<X;i++)
  {
    for(int j=y;j<Y;j++)
    {
      if(map[i,j] is Champignon && (i!=x || j!=y)) {
        int tmp = ProchainChampignon(x,y,score);
        if(tmp>currentBestScore) {
          tmp=currentBestScore;
          currentBestNext = map[i,j];
        }
      }
    }
  }
  return currentBestScore;
}
```
