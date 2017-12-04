﻿using System.Collections.Generic;
using System.Linq;
using Authentication.Models.Services;

namespace Authentication.Models.Models
{
    public class ConsumerKeys : IConsumerKeys
    {
        private IConsumerKeyService ConsumerKeyService { get; set; }
        private List<ConsumerKey> ConsumerKeysList { get; set; }

        public ConsumerKeys(IConsumerKeyService consumerKeyService)
        {
            ConsumerKeyService = consumerKeyService;
        }

        private void LoadConsumerKeys()
        {
            ConsumerKeysList = ConsumerKeyService.GetAllConsumerKeysSync().ToList();
        }

        public IEnumerable<ConsumerKey> GetAll()
        {
            if (ConsumerKeysList == null)
                LoadConsumerKeys();
            return ConsumerKeysList;
        }
    }
}