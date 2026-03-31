import { MessageCircle, X } from 'lucide-react';
import { useState } from 'react';
import { Button } from './ui/button';
import { Input } from './ui/input';

export function AIChatbot() {
  const [isOpen, setIsOpen] = useState(false);
  const [messages, setMessages] = useState<{ role: 'user' | 'bot'; text: string }[]>([
    {
      role: 'bot',
      text: "Hi! I'm your AI cooking assistant. What would you like to cook today? 👨‍🍳",
    },
  ]);
  const [input, setInput] = useState('');

  const suggestions = [
    'Quick dinner ideas',
    'Vegetarian recipes',
    'What can I make with chicken?',
    'Healthy meal prep',
  ];

  const handleSend = () => {
    if (!input.trim()) return;

    setMessages([...messages, { role: 'user', text: input }]);

    // Simulate AI response
    setTimeout(() => {
      const responses = [
        "Great choice! I recommend trying our Creamy Tuscan Chicken. It's quick and delicious!",
        "How about a Mediterranean Quinoa Bowl? It's healthy and packed with flavor!",
        "Our Pad Thai recipe is perfect for that! Would you like to see it?",
        "I found 12 recipes matching your criteria. Would you like to filter by cooking time?",
      ];
      const randomResponse = responses[Math.floor(Math.random() * responses.length)];
      setMessages((prev) => [...prev, { role: 'bot', text: randomResponse }]);
    }, 1000);

    setInput('');
  };

  const handleSuggestionClick = (suggestion: string) => {
    setInput(suggestion);
  };

  if (!isOpen) {
    return (
      <button
        onClick={() => setIsOpen(true)}
        className="fixed bottom-6 right-6 z-50 w-14 h-14 bg-primary hover:bg-primary/90 text-primary-foreground rounded-full shadow-lg flex items-center justify-center transition-all hover:scale-110"
      >
        <MessageCircle className="h-6 w-6" />
      </button>
    );
  }

  return (
    <div className="fixed bottom-6 right-6 z-50 w-80 sm:w-96 h-[500px] bg-card rounded-2xl shadow-2xl flex flex-col border border-border">
      {/* Header */}
      <div className="bg-primary text-primary-foreground p-4 rounded-t-2xl flex items-center justify-between">
        <div className="flex items-center space-x-2">
          <div className="w-8 h-8 bg-white/20 rounded-full flex items-center justify-center">
            <span>🤖</span>
          </div>
          <div>
            <h4 className="font-semibold">AI Chef Assistant</h4>
            <p className="text-xs opacity-90">Online</p>
          </div>
        </div>
        <Button
          variant="ghost"
          size="icon"
          onClick={() => setIsOpen(false)}
          className="text-primary-foreground hover:bg-white/20"
        >
          <X className="h-5 w-5" />
        </Button>
      </div>

      {/* Messages */}
      <div className="flex-1 overflow-y-auto p-4 space-y-3">
        {messages.map((message, index) => (
          <div
            key={index}
            className={`flex ${message.role === 'user' ? 'justify-end' : 'justify-start'}`}
          >
            <div
              className={`max-w-[80%] px-4 py-2 rounded-2xl ${
                message.role === 'user'
                  ? 'bg-primary text-primary-foreground'
                  : 'bg-muted text-foreground'
              }`}
            >
              <p className="text-sm">{message.text}</p>
            </div>
          </div>
        ))}

        {messages.length === 1 && (
          <div className="space-y-2 mt-4">
            <p className="text-xs text-muted-foreground text-center">Try asking:</p>
            {suggestions.map((suggestion, index) => (
              <button
                key={index}
                onClick={() => handleSuggestionClick(suggestion)}
                className="w-full text-left px-3 py-2 bg-muted hover:bg-accent rounded-lg text-sm transition-colors"
              >
                {suggestion}
              </button>
            ))}
          </div>
        )}
      </div>

      {/* Input */}
      <div className="p-4 border-t border-border">
        <div className="flex space-x-2">
          <Input
            value={input}
            onChange={(e) => setInput(e.target.value)}
            onKeyPress={(e) => e.key === 'Enter' && handleSend()}
            placeholder="Ask me anything..."
            className="flex-1"
          />
          <Button onClick={handleSend} size="icon">
            <span className="text-lg">→</span>
          </Button>
        </div>
      </div>
    </div>
  );
}
