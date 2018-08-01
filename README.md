## 使用的 Node編輯套件
https://github.com/aphex-/BrotherhoodOfNode

----------------------------------------------------------------------------------------

## AI編輯器
![Image](https://raw.githubusercontent.com/apperdog/EditorMonsterAI/master/show.png)



### 如何使用
1.將Assets複製進專案裡
<br>2.打開選單，window => MonsterAI Editor</br>



### 開始編輯
![Image](https://github.com/apperdog/EditorMonsterAI/blob/master/show3.png)
<br>1.Open，打開已儲存的檔案。</br>
2.Save，儲存編輯檔案，檔案以json格式儲存。儲存時會生成兩個檔案，檔案名默認為EditorData與EditorDataUseData。EditorData為編輯器使用的檔案。EditorDataUseData為進行遊戲使用的檔案。
3.New，建立新的編輯。



### 編輯狀態機
![Image](https://github.com/apperdog/EditorMonsterAI/blob/master/show2.png)
<br>1.MonsterAI => Control，建立狀態控制器。狀態控制器為，儲存該狀態執行的操作。執行的操作有，進入該狀態時、每次update時、離開該狀態時執行的方法。</br>
2.Monster => Value Condition，建立條件判斷。
<br>3.String => String，用來添加狀態控制器執行的方法名稱。</br>



### 還在完善中....
