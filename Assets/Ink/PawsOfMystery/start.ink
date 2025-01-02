// start.ink


LIST Characters = 주인공, 여우, 공작, 염소, 족제비, 해설

VAR currentSpeaker = Characters.해설

//
// System: inventory
//
LIST State = promiseCube


LIST Inventory = (none), seeds
        , foxFur, peacockFeather, goatFootprint
        , apple, giftBox, letter
        , massageToSparrow, shoppingListMemo, myself

VAR isGetAnyHintFoxRoom = false
VAR isGetAnyHintPeacockRoom = false
VAR isGetAnyHintGoatRoom = false

VAR foundFoxFur = 0
VAR foundPeacockFeather = 0
VAR foundGoatFootprint = 0

=== function getHintInFoxRoom()
    ~ isGetAnyHintFoxRoom = true
    
=== function getHintInPeacockRoom()
    ~ isGetAnyHintFoxRoom = true
    
=== function getHintInGoatRoom()
    ~ isGetAnyHintFoxRoom = true

=== function add_item(item)
	{ Inventory !? item :
         ~ Inventory += item 
    }

=== function remove_item(item)
	{ Inventory ? item :
         ~ Inventory -= item 
    }

=== function has_item(item)
    ~ return Inventory ? item

=== function print_inventory()
    ~ return Inventory


EXTERNAL DisplayHint(scene)
=== function DisplayHint(scene)
    DisplayHint + ' : ' + scene
    
EXTERNAL StartButtonAnimation()
=== function StartButtonAnimation()
    StartButtonAnimation


=== start ===

		오후의 긴 해가 창에서 넘어와 고양이 탐정 다스를 비췄다.
    다스는 따스한 햇살 아래 졸린 눈으로 창밖을 바라보며 한가로운 시간을 보내고 있었다.  
    멀리서 족제비가 허둥지둥 달려왔다.

    다쭈! 여기 있었구나! 
    (숨을 몰아쉬며) 너... 정말 한가하게 앉아있구나.

    * 테스트 푱
        -> goto_forest.sniff_around
    * 한가하다니 무례하네, 무슨 일인데 쭈? 
        -> ask_problem


        
= refuse_help
				~ currentSpeaker = Characters.족제비
		?!!
		정말 이대로 날 돕지 않을 거야? 햄스터에게 무슨 일이 생겼을지 모르잖아...
        ~ currentSpeaker = Characters.해설
    족제비는 어깨를 축 늘어뜨리며 깊게 한숨을 쉬었다.

        ~ currentSpeaker = Characters.주인공
    응 귀찮다쭈
    
        ~ currentSpeaker = Characters.족제비
    알았어, 다쭈... 하지만 네 마음이 바뀌면, 다시 와줘...
    
        ~ currentSpeaker = Characters.해설
		몇 시간이 지나고 족제비는 혼자서 숲을 헤매다 지쳐 돌아왔지만, 햄스터는 돌아오지 않았다. 
		
		
		BAD ENDING: 다스는 끝내 그날의 사건을 알지 못했다...
    
    //BAD ENDING "다스는 끝내 그날의 사건을 알지 못했다..."
-> END


= ask_problem
        ~ currentSpeaker = Characters.족제비
    햄스터가 안 보여! 실종된게 분명해 
    

 //   ~  DisplayHint("sniff_around")

//~ add_item(foxFur)




         ~ currentSpeaker = Characters.주인공
    햄스터가 누군데 쭈?
         ~ currentSpeaker = Characters.족제비
    우리 마을에 사는 친구야. 어제 씨앗을 모으러 간다고 숲으로 갔는데 아직도 돌아오질 않았어. 
    분명히 무슨 일이 생긴 게 틀림없어.

    * 마지막으로 본 게 언젠데 쭈? 
        -> last_seen
    * 글세 그냥 잠깐 길을 잃은 거겠지 쭈 
        -> lost_possibility
    * 킁...얼마까지 알아보셨나 쭈?
        -> how_much
    * 지금 바쁜데, 나중에 얘기하자 쭈.
        -> refuse_help

= last_seen
        ~ currentSpeaker = Characters.족제비
    어제 저녁쯤에 마지막으로 봤어. 그때도 무척 바빠 보였어.

    * 알겠어. 그럼 가자
        -> investigate_scene

= lost_possibility
        ~ currentSpeaker = Characters.족제비
    햄스터는 그런 일이 없었어! 항상 길을 잘 찾아다니는 아이야.

    * 그래, 그러면 다른 단서부터 찾아보자. 
        -> investigate_scene

= how_much
        ~ currentSpeaker = Characters.족제비
    ...
        ~ currentSpeaker = Characters.주인공
    ...
        ~ currentSpeaker = Characters.족제비
    ...츄르라도 달라는 뜻이지?
        ~ currentSpeaker = Characters.주인공
    모든 고양이가 츄르라면 좋아할 거란 고정관념을 버려라 쭈!
    나는 츄르는 안 먹는다쭈
        ~ currentSpeaker = Characters.족제비
    .. 뭘 원하는데?
        ~ currentSpeaker = Characters.주인공
		작고 네모나고 꼬소하고 짭짤한 간식, 큐브라고 부른다쭈.
		마미는 하루에 3개밖에 안준다쭈.
				~ currentSpeaker = Characters.족제비
		큐브? 그게 무슨 간식인데? 복잡하게 말하네...
		    ~ currentSpeaker = Characters.주인공
		큐브를 모르냐 쭈? 준비 못할거 같으면 그냥 관둬라 쭈!
			~ currentSpeaker = Characters.족제비
		아니야, 작고 꼬소하고 짭짤한 간식... 햄스터만 찾아준다면 준비해볼게.
    ~ add_item(promiseCube)
    
    * 냄새가 나는군. 사건의 냄새가.
        -> investigate_scene

= investigate_scene
        ~ currentSpeaker = Characters.해설
		다스는 일어나서 족제비에게 말을 한다.
		
        ~ currentSpeaker = Characters.주인공
    사건은 내가 맡겠다쭈!
		마미랑 쿠크한테 잠깐 나갔다 온다고만 말하면 된다쭈.  
		그 다음에 너의 사라진 친구가 가장 마지막까지 있었던 장소로 데려다주라 쭈.   
		
        ~ currentSpeaker = Characters.족제비
    그래, 어디로 갔는지는 대강 알거같아.
    
        ~ currentSpeaker = Characters.주인공
		냄새는 거짓말을 하지 않는다쭈.  
		그 친구가 마지막으로 있었던 곳이라면, 분명 무언가 남아 있을 거다쭈.
		
        ~ currentSpeaker = Characters.해설
    다스는 족제비와 함께 숲으로 향했다.
        -> goto_forest



-> goto_forest