using System;
using System.Security;

namespace NoiseMe.Drags.App.Models.CredentialManagement
{
	internal interface ICredentialsPrompt : IDisposable
	{
		string Username
		{
			get;
			set;
		}

		string Password
		{
			get;
			set;
		}

		SecureString SecurePassword
		{
			get;
			set;
		}

		string Title
		{
			get;
			set;
		}

		string Message
		{
			get;
			set;
		}

		bool SaveChecked
		{
			get;
			set;
		}

		bool GenericCredentials
		{
			get;
			set;
		}

		bool ShowSaveCheckBox
		{
			get;
			set;
		}

		int ErrorCode
		{
			get;
			set;
		}

		DialogResult ShowDialog();

		DialogResult ShowDialog(IntPtr owner);
	}
}
