# Project B

## Team members

r07521606 劉鎧禎
r06521605 許舜翔
r06521609 趙君傑

## Behavior of The Tower Crane
吊車的功能有旋轉rib、移動trolley、升降hook以及將東西吊起。
### 旋轉rib
>可以360度旋轉。

A(逆時針)
D(順時針)
![](https://i.imgur.com/HPJtf8N.gif)


### 移動trolley
>有設定不能前進到超出rib以及不能後退超出rib旋轉軸心。

W(前進)
S(後退)
![](https://i.imgur.com/6OiKIJX.gif)


### 升降hook
>有設定一個升的極限，以及降到碰到物體後不能再降。

Q(升)
E(降)
![](https://i.imgur.com/kD9MNh9.gif)


### 吊
> 感應到物體時，物體會呈現黃色，此時按下space可以將物體吊起，會有四條cable連接於物體及hook，再按下space可將物體放開。

space
![](https://i.imgur.com/UKGm2eW.gif)


### Joint 設定
> Joint為每個節點重要的連接元素，在吊車裡我們可以把連接關係簡單分為**trolley-hook**及**hook-baby**，以下會簡單描述之間的joint參數關係。

**1. trolley-hook**
trolley的GameObject(**trolley**)內有個Configurable Joint，此joint連接trolley內的另一個GameObject(**trolleyjoint**)，這兩個GameObject的目的為保持trolley本身的剛性，不會被外界力量帶走，因此**trolley**中joint的Anchor與Connected Anchor須在trolley的model內，XYZ Motion及Angular XYZ Motion皆為Locked；**trolleyjoint**內也有個Configurable Joint，此joint連接hook，因trolleyjoint與hook中的cable需可以自由伸縮，所以XYZ Motion為Limited，在使用者升降的同時會不斷更新此joint的Linear Limit。
**2. hook-baby**
與**trolley-hook**的關係相似，**hook**中的joint連接另一個GameObject(**joint-body**)，為了保持hook的剛性，hook中joint的Anchor與Connected Anchor須在trolley的model內，XYZ Motion及Angular XYZ Motion皆為Locked；**joint-body**要連接至使用者欲吊之物體，因Unity的Configurable Joint若建立時沒給予一Connected Body，Unity會接合至某Fixed Point，會造成物理影響，所以**joint-body**的joint為使用者吊物體的同時用Script新增。
![](https://i.imgur.com/jGzdUVf.png)


## Scene and Camera
為了讓使用者更加融入場景中，我們精心挑選了生活中必備的傢俱，並將其排列至主要的嬰兒床場景附近，作為遊戲中的背景。

我們利用了free3D網站中找到傢具模型和一些網路上的資源建置我們的場景。
>Ref:    
>https://free3d.com/3d-model/baby-crib-83588.html
https://assetstore.unity.com/packages/3d/props/furniture/realistic-furniture-and-interior-props-pack-120379


是一個溫馨的小套房，採用了搞笑漫畫作為牆壁壁紙。

![](https://i.imgur.com/3Dao8x0.jpg)

中間的嬰兒床是我們吊車主要的操作場景。

![](https://i.imgur.com/YkFgBGx.jpg)

一些必備的生活傢俱。

![](https://i.imgur.com/lUiBey5.jpg)

### 吊物模型 - 嬰兒
一般來說，吊車只會出現在營造工程上，雖然現今美好的環境多虧了他，但普羅大眾對吊車仍很陌生，因此，我們希望能顛覆吊車金屬感、冰冷、沈重的形象，為他加上一些可愛、活潑的元素，那當提到可愛活潑時，一定馬上想到的就是－－嬰兒，我們讓玩家扮演嬰兒，從他的視角去操作吊車，就好像是在玩玩具一般，大幅降低吊車給人的距離感！

我們同樣在free3D上找到嬰兒的模型

>Ref:    https://free3d.com/3d-model/playingbaby-v1--486725.html

![](https://i.imgur.com/u2JRjcX.jpg)

那我們認為一個嬰兒還不夠可愛，且吊車需要有目標來進行吊取的工作，所以我們決定也用嬰兒當作目標物，一個嬰兒不夠你有沒有試過一堆。

![](https://i.imgur.com/KiMf4lJ.jpg)

在操作起來也相當療癒。

![](https://i.imgur.com/B8v25ki.jpg)

### 多重視角
雖然嬰兒很可愛，但嬰兒因為受限於年齡，視線上比較難操作吊車，因此，我們也加入了三個視角提供作切換，讓大家能更加享受吊車的樂趣！(按左ctrl切換視角一/二)

>視角一：嬰兒固定視角（難度－☆★★★★）

![](https://i.imgur.com/G6OQWtl.jpg)

>視角二：上帝(自由)視角（難度－☆☆☆★★）

![](https://i.imgur.com/UbDTwX0.jpg)

>視角三：俯視視角－輔助用小地圖（難度－☆☆☆☆★）

![](https://i.imgur.com/66gobAQ.jpg)

## Gamification

### Game UI

![](https://i.imgur.com/9kTYFN3.png)
遊戲開始按鈕

![](https://i.imgur.com/yceaMOR.png)
遊戲資訊UI
> 1. 顯示時間倒數
> 2. 顯示獲得的分數
### Rule
遊戲一開始隨機產生嬰兒(吊物)，透過延遲0.3秒的方式避免產生時Collider相互碰撞是物件炸裂
* ProjectBGameMain.cs
```csharp
IEnumerator CreateBaby () {
    float x = Random.Range (randomBabyCenter.position.x - randomPositionRange, randomBabyCenter.position.x + randomPositionRange);
    float y = Random.Range (randomBabyCenter.position.y - randomPositionRange, randomBabyCenter.position.y + randomPositionRange);
    Instantiate (baby, new Vector3 (x, y, randomBabyCenter.position.z), Quaternion.identity);
    yield return new WaitForSeconds (0.3f);
    babyNumber--;
    if (babyNumber > 0) {
        StartCoroutine (CreateBaby ());
    }
}
```
透過嬰兒(吊物)物件上掛有Baby tag
![](https://i.imgur.com/6iVggoe.png)
讓床(目標)的腳本判斷是否為嬰兒後會回傳控制器加分並隨機改變床的位置，接著刪除投入成功的嬰兒
* ProjectBBed.cs
```csharp
void OnTriggerEnter (Collider other) {
    if (other.gameObject.tag == ProjectBConfig.BABY_TAG) {
        rg.velocity = Vector3.zero;
        ProjectBGameMain.Instance.GetScore ();
        Destroy (other.gameObject);
    }
}
```
* ProjectBGameMain.cs
```csharp
public void GetScore () {
    scoreText.rectTransform.localScale = textScale;
    scoreText.rectTransform.DOScale (new Vector3 (1, 1, 1), 0.5f).SetEase (textEase);
    score++;
    scoreText.text = score.ToString ();
    BedRandomPosition ();
}
private void BedRandomPosition () {
    float x = Random.Range (randomBedCenter.position.x - randomPositionRange, randomBedCenter.position.x + randomPositionRange);
    float y = Random.Range (randomBedCenter.position.y - randomPositionRange, randomBedCenter.position.y + randomPositionRange);
    bed.position = new Vector3 (x, y, randomBedCenter.position.z);
}
```

### Work Division

Behavior of the tower crane: 劉鎧禎
Scene and camera: 許舜翔
Gamification: 趙君傑
