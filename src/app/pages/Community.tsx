import { Users, MessageSquare, Award, TrendingUp } from 'lucide-react';
import { Button } from '../components/ui/button';
import { recipes } from '../data/recipes';

export function Community() {
  return (
    <div className="min-h-screen">
      <div className="bg-gradient-to-br from-primary/10 to-secondary/10 py-12">
        <div className="container mx-auto px-4">
          <div className="flex items-center space-x-3 mb-4">
            <Users className="h-10 w-10 text-primary" />
            <h1>Community</h1>
          </div>
          <p className="text-muted-foreground">
            Connect with fellow food lovers and share your culinary journey
          </p>
        </div>
      </div>

      <div className="container mx-auto px-4 py-12">
        {/* Stats */}
        <div className="grid grid-cols-1 md:grid-cols-4 gap-6 mb-12">
          <div className="bg-card rounded-2xl p-6 border border-border text-center">
            <div className="w-12 h-12 bg-primary/10 rounded-full flex items-center justify-center mx-auto mb-3">
              <Users className="h-6 w-6 text-primary" />
            </div>
            <h3 className="text-2xl mb-1">50K+</h3>
            <p className="text-muted-foreground text-sm">Active Members</p>
          </div>
          <div className="bg-card rounded-2xl p-6 border border-border text-center">
            <div className="w-12 h-12 bg-secondary/10 rounded-full flex items-center justify-center mx-auto mb-3">
              <MessageSquare className="h-6 w-6 text-secondary" />
            </div>
            <h3 className="text-2xl mb-1">100K+</h3>
            <p className="text-muted-foreground text-sm">Recipes Shared</p>
          </div>
          <div className="bg-card rounded-2xl p-6 border border-border text-center">
            <div className="w-12 h-12 bg-primary/10 rounded-full flex items-center justify-center mx-auto mb-3">
              <Award className="h-6 w-6 text-primary" />
            </div>
            <h3 className="text-2xl mb-1">500K+</h3>
            <p className="text-muted-foreground text-sm">Reviews Posted</p>
          </div>
          <div className="bg-card rounded-2xl p-6 border border-border text-center">
            <div className="w-12 h-12 bg-secondary/10 rounded-full flex items-center justify-center mx-auto mb-3">
              <TrendingUp className="h-6 w-6 text-secondary" />
            </div>
            <h3 className="text-2xl mb-1">1M+</h3>
            <p className="text-muted-foreground text-sm">Recipe Views</p>
          </div>
        </div>

        {/* Recent Activity */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
          <div className="lg:col-span-2">
            <h2 className="mb-6">Recent Activity</h2>
            <div className="space-y-4">
              {recipes.slice(0, 5).map((recipe) => (
                <div
                  key={recipe.id}
                  className="bg-card rounded-2xl p-6 border border-border hover:shadow-md transition-shadow"
                >
                  <div className="flex items-start space-x-4">
                    <div className="w-10 h-10 bg-primary rounded-full flex items-center justify-center text-primary-foreground flex-shrink-0">
                      U
                    </div>
                    <div className="flex-1">
                      <div className="flex items-center space-x-2 mb-2">
                        <span className="font-medium">User{recipe.id}</span>
                        <span className="text-muted-foreground text-sm">shared a recipe</span>
                        <span className="text-muted-foreground text-sm">2h ago</span>
                      </div>
                      <div className="flex items-center space-x-3 bg-muted/50 rounded-xl p-3">
                        <img
                          src={recipe.image}
                          alt={recipe.title}
                          className="w-16 h-16 object-cover rounded-lg"
                        />
                        <div>
                          <h4 className="text-sm mb-1">{recipe.title}</h4>
                          <p className="text-xs text-muted-foreground line-clamp-1">
                            {recipe.description}
                          </p>
                        </div>
                      </div>
                      <div className="flex items-center space-x-4 mt-3 text-sm text-muted-foreground">
                        <button className="hover:text-primary transition-colors">
                          ❤️ 45 likes
                        </button>
                        <button className="hover:text-primary transition-colors">
                          💬 12 comments
                        </button>
                        <button className="hover:text-primary transition-colors">
                          🔖 Save
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          </div>

          {/* Sidebar */}
          <div className="lg:col-span-1">
            <div className="sticky top-20 space-y-6">
              {/* Top Contributors */}
              <div className="bg-card rounded-2xl p-6 border border-border">
                <h3 className="mb-4">Top Contributors</h3>
                <div className="space-y-4">
                  {[1, 2, 3, 4, 5].map((user) => (
                    <div key={user} className="flex items-center justify-between">
                      <div className="flex items-center space-x-3">
                        <div className="w-10 h-10 bg-primary rounded-full flex items-center justify-center text-primary-foreground">
                          {user}
                        </div>
                        <div>
                          <h4 className="text-sm">Chef{user}</h4>
                          <p className="text-xs text-muted-foreground">
                            {Math.floor(Math.random() * 50) + 10} recipes
                          </p>
                        </div>
                      </div>
                      <Button size="sm" variant="outline">
                        Follow
                      </Button>
                    </div>
                  ))}
                </div>
              </div>

              {/* Join CTA */}
              <div className="bg-gradient-to-br from-primary to-secondary rounded-2xl p-6 text-white">
                <h3 className="text-white mb-2">Join Our Community</h3>
                <p className="text-white/90 text-sm mb-4">
                  Share your recipes and connect with food lovers worldwide
                </p>
                <Button variant="secondary" className="w-full">
                  Get Started
                </Button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
