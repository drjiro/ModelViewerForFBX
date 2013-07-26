#region File Description
//-----------------------------------------------------------------------------
// ErrorLogger.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System.Collections.Generic;
using Microsoft.Build.Framework;
#endregion

namespace WinFormsContentLoading
{
    /// <summary>
    /// MSBuild ILogger インターフェイスをカスタムに実装し、ユーザーが後から
    /// 確認できるように、コンテンツ ビルド エラーを記録します。
    /// </summary>
    class ErrorLogger : ILogger
    {
        /// <summary>
        /// ErrorRaised 通知イベントをフックして、カスタム ロガーを初期化します。
        /// </summary>
        public void Initialize(IEventSource eventSource)
        {
            if (eventSource != null)
            {
                eventSource.ErrorRaised += ErrorRaised;
            }
        }


        /// <summary>
        /// カスタム ロガーをシャットダウンします。
        /// </summary>
        public void Shutdown()
        {
        }


        /// <summary>
        /// エラー メッセージ文字列を格納することによって、エラー通知イベントを処理します。
        /// </summary>
        void ErrorRaised(object sender, BuildErrorEventArgs e)
        {
            errors.Add(e.Message);
        }


        /// <summary>
        /// 記録されたすべてのエラーのリストを取得します。
        /// </summary>
        public List<string> Errors
        {
            get { return errors; }
        }

        List<string> errors = new List<string>();


        #region ILogger Members

        
        /// <summary>
        /// ILogger.Parameters プロパティを実装します。
        /// </summary>
        string ILogger.Parameters
        {
            get { return parameters; }
            set { parameters = value; }
        }

        string parameters;


        /// <summary>
        /// ILogger.Verbosity プロパティを実装します。
        /// </summary>
        LoggerVerbosity ILogger.Verbosity
        {
            get { return verbosity; }
            set { verbosity = value; }
        }

        LoggerVerbosity verbosity = LoggerVerbosity.Normal;


        #endregion
    }
}
