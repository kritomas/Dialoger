/*
 *	MIT License
 *
 *	Copyright (c) 2024 kritomas
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System.Text.Json;

/// <summary>
/// Namespace for dialog utilities.
/// </summary>
namespace dialoger
{
	/// <summary>
	/// Response in a dialog.
	/// </summary>
	class Response
	{
		public string text { get; set; } = "";
		public int goTo { get; set; } = -1;

		public Response(string text, int goTo = -1)
		{
			this.text = text;
			this.goTo = goTo;
		}

		public override string ToString()
		{
			return text;
		}
	}

	/// <summary>
	/// Statement in a dialog.
	/// </summary>
	class Statement
	{
		public string message { get; set; } = "";
		public List<Response> responses { get; set; } = new List<Response>();

		public Response this[int i]
		{
			get { return responses[i]; }
			set { responses[i] = value; }
		}

		public override string ToString()
		{
			return message;
		}
	}

	/// <summary>
	/// Dialog itself.
	/// </summary>
	class Dialog
	{
		public List<Statement> statements { get; set; } = new List<Statement>();
		public int current = 0;

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The current statement.</returns>
		public Statement Now()
		{
			return statements[current];
		}
		/// <summary>
		/// Answers to the current statement.
		/// </summary>
		/// <param name="responseIndex">The index of the response to the statement.</param>
		public void Answer(int responseIndex)
		{
			current = Now().responses[responseIndex].goTo;
		}
		/// <summary>
		/// Resets the dialog to the beginning.
		/// </summary>
		public void Reset()
		{
			current = 0;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns>The dialog is still running (true), or the dialog had already ended (false).</returns>
		public bool Talking()
		{
			if (current < 0 || current >= statements.Count) return false;
			return true;
		}

		public Statement this[int i]
		{
			get { return statements[i]; }
			set { statements[i] = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>The dialog as JSON.</returns>
		public string Json()
		{
			return JsonSerializer.Serialize(this);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="json">The JSON to parse.</param>
		/// <returns>Dialog from the JSON.</returns>
		public static Dialog? ParseJson(string json)
		{
			return JsonSerializer.Deserialize<Dialog>(json);
		}
	}
}
