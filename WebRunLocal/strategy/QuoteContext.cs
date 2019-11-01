using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebRunLocal.dto;
using WebRunLocal.strategy;

namespace WebRunLocal.strategy
{
    //策略模式上下文
    public class QuoteContext
    {
        //持有一个具体的策略
        private IQuoteStrategy quoteStrategy;

        //回调具体的策略方法
        public void invokeLocalApp(InputDTO inputDTO, OutputDTO outputDTO, string localAppPath) 
        {
            if (inputDTO.type == 1)
            {
                quoteStrategy = new CallDLLStrategy();
            }
            else if (inputDTO.type == 2)
            {
                quoteStrategy = new CallExeStrategy();
            }
            else 
            {
                throw new Exception("【调用类型不正确，请检查，TYPE = " + inputDTO.type + "】");
            }

            this.quoteStrategy.invokeLocalApp(inputDTO, outputDTO, localAppPath);
        }
    }
}
