using System;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
public class InkManager
{
    private static InkManager _instance; // 싱글톤 인스턴스
    public Story story; // Ink 스토리 객체

    public static InkManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InkManager();
            }
            return _instance;
        }
    }

    private InkManager()
    {
        // 생성자에서 스토리를 초기화하거나 설정 가능
    }

    // Ink 스토리 초기화
    public Story InitializeStory(string inkJson)
    {
        if (story == null) // 이미 초기화된 경우 재초기화 방지
        {
            if (!string.IsNullOrEmpty(inkJson))
            {
                story = new Story(inkJson);
                Console.WriteLine("Ink 스토리 초기화 완료");
            }
            else
            {
                throw new Exception("Ink JSON 데이터가 비어있습니다.");
            }
        }
        else
        {
            Console.WriteLine("Ink 스토리가 이미 초기화되었습니다.");
        }
        return story;
    }

    // Ink 스토리에서 다음 줄 가져오기
    public string ContinueStory()
    {
        if (story != null && story.canContinue)
        {
            return story.Continue();
        }
        return null;
    }

    // Ink 선택지 가져오기
    public List<string> GetChoices()
    {
        List<string> choices = new List<string>();
        if (story != null && story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                choices.Add(choice.text);
            }
        }
        return choices;
    }

    // 선택지 선택
    public void MakeChoice(int choiceIndex)
    {
        if (story != null && choiceIndex >= 0 && choiceIndex < story.currentChoices.Count)
        {
            story.ChooseChoiceIndex(choiceIndex);
        }
        else
        {
            throw new Exception("유효하지 않은 선택지 인덱스입니다.");
        }
    }

    // Ink 변수 가져오기
    public string GetVariable(string variableName)
    {

        string currentSpeaker = story.variablesState[variableName]?.ToString();

        if (string.IsNullOrEmpty(currentSpeaker))
        {
            return currentSpeaker;
        }
        throw new Exception($"변수 {variableName}를 찾을 수 없습니다.");
    }


    public List<string> GetVariableAsList(string variableName)
    {
        if (story == null)
        {
            throw new Exception("Story object is null.");
        }


        var variable = story.EvaluateFunction("print_inventory");

        // var variable = story.variablesState[variableName];
        if (variable != null) Debug.Log($"Variable '{variableName + ": " + variable}' 타입: {variable.GetType()}");
        Debug.Log($"Variable '{variableName}' 타입: {variable.GetType()}");
        List<string> result = new List<string>();

        // InkList 처리
        if (variable is Ink.Runtime.InkList inkList)
        {

            foreach (var item in inkList.Keys)
            {
                result.Add(item.ToString());
            }

            return result;
        }
        else if (variable is System.String str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArr = str.Split(',');
                foreach (string item in strArr)
                {
                    result.Add(item.ToString());
                }
            }
            return result;
        }
        else
        {
            throw new Exception($"Variable '{variableName}' is not a list.");
        }
    }
    // 스토리 이동
    public void ChoosePathString(string path)
    {
        story.ChoosePathString(path);
    }

    public void AddItemToInventory(string item)
    {
        if (story != null)
        {
            try
            {
                // 현재 Inventory 상태 가져오기
                var inventory = story.variablesState["Inventory"] as Ink.Runtime.InkList;

                if (inventory == null)
                {
                    // Inventory 초기화
                    inventory = new Ink.Runtime.InkList("Inventory", story);
                }

                // 아이템 추가
                inventory.AddItem(item);

                // 업데이트된 Inventory를 다시 Ink로 할당
                story.variablesState["Inventory"] = inventory;

                Debug.Log($"Item '{item}' added to inventory. Current inventory: {inventory}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to add item to inventory. Error: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError("Story object is null. Cannot add item.");
        }
    }

    // Ink 변수 설정
    public void SetVariable(string variableName, object value)
    {
        if (story != null)
        {
            story.variablesState[variableName] = value;
        }
        else
        {
            throw new Exception("스토리가 초기화되지 않았습니다.");
        }
    }

    // Ink에서 특정 태그 가져오기
    public List<string> GetTags()
    {
        if (story != null)
        {
            return story.currentTags;
        }
        return new List<string>();
    }
}