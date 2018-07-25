using System;
using Assets.Code.Bon.Socket;
using UnityEngine;

namespace Assets.Code.Bon.Nodes.String
{
	[Serializable]
	[GraphContextMenuItem("String", "String")]
	public class StringNode : AbstractStringNode {

		[SerializeField] private string _text = "";
		[NonSerialized] private Rect _textFieldRect;
    [NonSerialized] private OutputSocket outputSocket;

		public StringNode(int id, Graph parent) : base(id, parent)
		{
			_textFieldRect = new Rect(3, 0, 100, 20);
      outputSocket = new OutputSocket(this, typeof(AbstractStringNode));
      Sockets.Add(outputSocket);
			Height = 45;
			Width = 108;
		}

		public override void OnGUI()
		{
			string newText = GUI.TextField(_textFieldRect, _text);

			if (!newText.Equals(_text)) TriggerChangeEvent();

      if (_text != newText)
      {
        _text = newText;
        CallUpdate();
      }
		}

		public override void Update()
		{
 
		}

    public void CallUpdate()
    {
      if (outputSocket.IsConnected())
      {
        Node node = (Node)outputSocket.GetConnectedSocket(0).Parent;
        node.Update();
      }
    }

		public override string GetString(OutputSocket outSocket)
		{
			return _text;
		}
	}
}
