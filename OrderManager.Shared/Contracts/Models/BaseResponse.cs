using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Shared.Contracts.Responses
{
    public class ApiResponse<TData>
    {
        public TData Response { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string> Errors { get; set; } = [];

        public bool Success { get; set; } = false;

    }

}
